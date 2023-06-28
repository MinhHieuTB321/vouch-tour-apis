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

        /// <summary>
        /// Get order by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var result = await _orderService.GetOrderById(id);
            return Ok(result);
        }

        /// <summary>
        /// Get order by phone number
        /// </summary>
        [HttpGet("{phonenumber}")]
        public async Task<IActionResult> GetOrderByPhone(string phonenumber)
        {
            var result = await _orderService.GetOrderByPhone(phonenumber);
            return Ok(result);
        }

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
