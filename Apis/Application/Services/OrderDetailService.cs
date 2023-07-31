using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;

        public OrderDetailService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }
        public async Task<bool> UpdateOrderDetail(OrderDetailUpdateDTO updateDTO)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(updateDTO.Id);
            if (orderDetail == null)
            {
                throw new NotFoundException("Order detail is not exist!");
            }

            orderDetail = _mapper.Map(updateDTO, orderDetail);
            _unitOfWork.OrderDetailRepository.Update(orderDetail);
            return await _unitOfWork.SaveChangeAsync() >0;
        }
    }
}
