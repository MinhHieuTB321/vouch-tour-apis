using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.GroupDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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

        public OrderService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }
        public async Task<bool> CreateOrder(OrderCreateDTO orderCreate)
        {
            var group= await _unitOfWork.GroupRepository.GetByIdAsync(orderCreate.GroupId);
            if (group == null) throw new NotFoundException("Not Found Group " + orderCreate.GroupId);

            var createDTO = _mapper.Map<Order>(orderCreate);
            createDTO.TotalPrice = GetTotalPrice(orderCreate.OrderProductDetails);
            var result = await _unitOfWork.OrderRepository.AddAsync(createDTO);
            AddOrderDetail(result.Id, orderCreate.OrderProductDetails);
            await AddPayment(result.Id);
            return await _unitOfWork.SaveChangeAsync()>0;
        }
        private async Task AddPayment(Guid orderId)
        {
            var payment = new Payment
            {
                OrderId = orderId,
                PaymentName = "Not",
                Status = OrderEnums.Waiting.ToString(),
            };
            await _unitOfWork.PaymentRepository.AddAsync(payment);
        }
        private double GetTotalPrice(List<OrderDetailCreateDTO> orderProducts)
        {
            var totalPrice = orderProducts.Sum(x => x.UnitPrice * x.Quantity);
            return totalPrice;
        }

        private async void AddOrderDetail(Guid orderId,List<OrderDetailCreateDTO> orderDetailCreateDTOs)
        {
            var createList = _mapper.Map<List<OrderDetail>>(orderDetailCreateDTOs);
            for (int i = 0; i < createList.Count; i++)
            {
                createList[i].OrderId = orderId;
            }
            await _unitOfWork.OrderDetailRepository.AddRangeAsync(createList);
        }

        public async Task<OrderViewDTO> GetOrderById(Guid orderId)
        {
            var order=await _unitOfWork.OrderRepository.GetByIdAsync(orderId,x=>x.Group,x=>x.OrderDetails,x=>x.Payments);
            if (order == null) throw new NotFoundException("Not Found Order");
            var result= _mapper.Map<OrderViewDTO>(order);
            result.OrderDetails=_mapper.Map<List<OrderDetailViewDTO>>(order.OrderDetails);
            result.PaymentName = order.Payments.First().PaymentName;
            return result;
        }

        public async Task<bool> UpdateOrder(OrderUpdateDTO updateDTO)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(updateDTO.Id);
            if (order == null) throw new BadRequestException("Order is not exist!");
            order.Status = OrderEnums.Complete.ToString();
            _unitOfWork.OrderRepository.Update(order);
            await UpdatePayment(updateDTO.Id);
            var result= await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }

        private async Task UpdatePayment(Guid orderId)
        {
            var payment = await _unitOfWork.PaymentRepository.FindByField(x => x.OrderId == orderId);
            payment.Status=OrderEnums.Complete.ToString();
            payment.PaymentName = "Cash";
            _unitOfWork.PaymentRepository.Update(payment);
        }
     
        public async Task<bool> DeleteOrder(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null) throw new BadRequestException("Order is not exist!");
            _unitOfWork.OrderRepository.SoftRemove(order);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }

        public async Task<OrderViewDTO> GetOrderByPhone(string phoneNumber)
        {
            var order = await _unitOfWork.OrderRepository.FindByField(x=>x.PhoneNumber==phoneNumber, x => x.Group, x => x.OrderDetails, x => x.Payments);
            if (order == null) throw new NotFoundException("Not Found Order");
            var result = _mapper.Map<OrderViewDTO>(order);
            result.OrderDetails = _mapper.Map<List<OrderDetailViewDTO>>(order.OrderDetails);
            result.PaymentName = order.Payments.First().PaymentName;
            return result;
        }

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
                var order = await _unitOfWork.OrderRepository.FindListByField(x => x.GroupId == group.Id && x.IsDeleted==false, x => x.OrderDetails);
                order.ForEach(x =>
                {
                    var addDTO = _mapper.Map<OrderViewDTO>(x);
                    addDTO.OrderDetails = _mapper.Map<List<OrderDetailViewDTO>>(x.OrderDetails);
                    addDTO.PaymentName = x.Payments.First().PaymentName;
                    result.Add(addDTO);
                });
            }
           
            return result;
        }
    }

}
