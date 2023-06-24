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
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork,
                            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }

}
