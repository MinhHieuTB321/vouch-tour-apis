﻿using Application.Commons;
using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.GroupDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FireSharp.Config;
using FireSharp.Interfaces;
using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _jobClient;
        private readonly IConfiguration _config;
        private readonly IFirebaseConfig _fireBaseConfig;
        private readonly IFirebaseClient _client;

        public OrderService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IClaimsService claimsService,
                            IBackgroundJobClient jobClient,
                            IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
            _jobClient = jobClient;
            _config = config;
            _fireBaseConfig = new FirebaseConfig
            {
                AuthSecret = _config["RealTimeDatabase:AuthSecret"],
                BasePath = _config["RealTimeDatabase:BasePath"],
            };
            _client = new FireSharp.FirebaseClient(_fireBaseConfig);
        }

        #region Create Order
        public async Task<bool> CreateOrder(OrderCreateDTO orderCreate)
        {
            var group= await _unitOfWork.GroupRepository.GetByIdAsync(orderCreate.GroupId);
            if (group == null) throw new NotFoundException("Not Found Group " + orderCreate.GroupId);

            var createDTO = _mapper.Map<Order>(orderCreate);
            createDTO.TourGuideId=group.TourGuideId;
            createDTO.TotalPrice = GetTotalPrice(orderCreate.OrderProductDetails);
            var result = await _unitOfWork.OrderRepository.AddAsync(createDTO);
            AddOrderDetail(result.Id, orderCreate.OrderProductDetails,group.TourGuideId);
            await AddPayment(result.Id);
            _jobClient.Enqueue(() => NotifiTourguide(group.TourGuideId, orderCreate.CustomerName));
            return await _unitOfWork.SaveChangeAsync()>0;
        }

        public async Task NotifiTourguide(Guid id,String cusName)
        {
            await FirebaseDatabase.SendNotification(_client, id, "You have a new order", $"Request from {cusName}");
        }
        public async Task AddPayment(Guid orderId)
        {
            var payment = new Payment
            {
                OrderId = orderId,
                PaymentName = "Not",
                Status = OrderEnums.Waiting.ToString(),
            };
            await _unitOfWork.PaymentRepository.AddAsync(payment);
        }
        public double GetTotalPrice(List<OrderDetailCreateDTO> orderProducts)
        {
            var totalPrice = orderProducts.Sum(x => x.UnitPrice * x.Quantity);
            return totalPrice;
        }

        public async void AddOrderDetail(Guid orderId,List<OrderDetailCreateDTO> orderDetailCreateDTOs,Guid tourGuideId)
        {
            var createList = _mapper.Map<List<OrderDetail>>(orderDetailCreateDTOs);
            for (int i = 0; i < createList.Count; i++)
            {
                createList[i].TourGuideId = tourGuideId;
                createList[i].OrderId = orderId;
            }
            await _unitOfWork.OrderDetailRepository.AddRangeAsync(createList);
        }

        #endregion

        #region Get Order By id
        public async Task<OrderViewDTO> GetOrderById(Guid orderId)
        {
            var order=await _unitOfWork.OrderRepository.GetByIdAsync(orderId,x=>x.Group,x=>x.OrderDetails,x=>x.Payments);
            if (order == null) throw new NotFoundException("Not Found Order");
            var result= _mapper.Map<OrderViewDTO>(order);
            result.OrderDetails=await GetListOrderDetailViews(order);
            result.PaymentName = order.Payments.First().PaymentName;
            return result;
        }
        #endregion


        private async Task<List<OrderDetailViewDTO>> GetListOrderDetailViews(Order order)
        {
            var listId= order.OrderDetails.Select(x=>x.Id).ToList();
            var orderDetails= await _unitOfWork.OrderDetailRepository.FindListByField(x=>listId.Contains(x.Id),x=>x.Product,x=>x.Order);
            var result= _mapper.Map<List<OrderDetailViewDTO>>(orderDetails);
            return result;
        }

        #region Update Order
        public async Task<bool> UpdateOrder(OrderUpdateDTO updateDTO)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(updateDTO.Id,x=>x.OrderDetails);
            if (order == null) throw new BadRequestException("Order is not exist!");
            order.Status = updateDTO.Status;
            order.ShipAddress= updateDTO.ShipAddress;
            _unitOfWork.OrderRepository.Update(order);
            if (updateDTO.Status.Equals(OrderEnums.Completed.ToString()))
            {
                await UpdatePayment(updateDTO.Id);
            }
            var result= await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }

        private async Task UpdatePayment(Guid orderId)
        {
            var payment = await _unitOfWork.PaymentRepository.FindByField(x => x.OrderId == orderId);
            payment.Status=OrderEnums.Completed.ToString();
            payment.PaymentName = "Cash";
            _unitOfWork.PaymentRepository.Update(payment);
        }
        #endregion

        #region Delete Order By Id
        public async Task<bool> DeleteOrder(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null) throw new BadRequestException("Order is not exist!");
            _unitOfWork.OrderRepository.SoftRemove(order);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }
        #endregion

        #region GetOrderByPhone
        public async Task<List<OrderViewDTO>> GetOrderByPhone(string phoneNumber)
        {
            var orders = await _unitOfWork.OrderRepository.FindListByField(x=>x.PhoneNumber==phoneNumber, x => x.Group, x => x.OrderDetails, x => x.Payments);
            if (orders.Count==0) throw new NotFoundException("Not Found Order");
            var result = await MapperOrderView(orders);
            return result;
        }

        private async Task<List<OrderViewDTO>> MapperOrderView(List<Order> orders)
        {
            var result = _mapper.Map<List<OrderViewDTO>>(orders);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].OrderDetails = await GetListOrderDetailViews(orders[i]);
                result[i].PaymentName = orders[i].Payments.First().PaymentName;
            }
            return result;
        }
        #endregion

        #region GetAl Order of Tour
        public async Task<List<OrderViewDTO>> GetAllOrderOfTourGuide()
        {
            var groups = await _unitOfWork.GroupRepository.FindListByField(x => x.TourGuideId == _claimsService.GetCurrentUser && x.IsDeleted==false,x=>x.Orders);
            if (groups.Count == 0) throw new BadRequestException("There is no groups");
            var result = await GetOrdersViewDTO(groups);
            return result;
        }

        private async Task<List<OrderViewDTO>> GetOrdersViewDTO(List<Group> groups)
        {
            var result = new List<OrderViewDTO>();

            foreach (var group in groups)
            {
                var order = await _unitOfWork.OrderRepository.FindListByField(x => x.GroupId == group.Id && x.IsDeleted==false, x => x.OrderDetails,x=>x.Payments,x=>x.Group);
                result.AddRange(await MapperOrderView(order));
            }
            return result;
        }
        #endregion  
    }

}
