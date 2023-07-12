using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Application.ViewModels.CartDTO;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class OrderController:BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create order
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateDTO addDTO)
        {
            await _orderService.CreateOrder(addDTO);
            return Ok("Adding successfully!");
        }

        #region GET
        /// <summary>
        /// Get all  order of tour-guide
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet]
        public async Task<IActionResult> GetAllOrderOfTourGuide()
        {
            var result = await _orderService.GetAllOrderOfTourGuide();
            return Ok(result);
        }

        /// <summary>
        /// Get order by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var result = await _orderService.GetOrderById(id);
            return Ok(result);
        }
        #endregion

        /// <summary>
        ///Update Order Status
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderUpdateDTO updateDTO)
        {
            await _orderService.UpdateOrder(updateDTO);
            return NoContent();
        }


        /// <summary>
        ///Delete Order
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrder(id);
            return Ok("Delete Successfully!");
        }
    }
}
