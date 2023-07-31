using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.SupplierDTO;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class OrderDetailController:BaseController
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        /// <summary>
        /// Update order detail
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.Supplier))]
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(OrderDetailUpdateDTO updateDTO)
        {
            var result = await _orderDetailService.UpdateOrderDetail(updateDTO);
            if (result)
            {
                return NoContent();
            }
            throw new Exception("Not Found!");
        }
    }
}
