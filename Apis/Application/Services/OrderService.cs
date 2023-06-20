using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.GroupDTOs;
using AutoMapper;
using Domain.Entities;
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
                            IClaimsService claimsService,
                            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
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
            
            return await _unitOfWork.SaveChangeAsync()>0;
        }

        private double GetTotalPrice(List<OrderDetailCreateDTO> orderProducts)
        {
            var totalPrice = orderProducts.Sum(x => x.UnitPrice * x.Quantity);
            return totalPrice;
        }

        private async void AddOrderDetail(Guid orderId,List<OrderDetailCreateDTO> orderProductDetailDTOs)
        {
            var createList = _mapper.Map<List<OrderDetail>>(orderProductDetailDTOs);
            for (int i = 0; i < createList.Count; i++)
            {
                createList[i].OrderId = orderId;
            }
            await _unitOfWork.OrderDetailRepository.AddRangeAsync(createList);
        }

        public async Task<OrderViewDTO> GetOrderById(Guid orderId)
        {
            var order=await _unitOfWork.OrderRepository.GetByIdAsync(orderId,x=>x.Group,x=>x.OrderDetails);
            if (order == null) throw new NotFoundException("Not Found Order");
            var result= _mapper.Map<OrderViewDTO>(order);
            result.Group=_mapper.Map<GroupViewDTO>(order.Group);
            result.OrderDetails=_mapper.Map<List<OrderDetailViewDTO>>(order.OrderDetails);
            return result;
        }
    }

}
