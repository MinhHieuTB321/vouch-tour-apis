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
        public async Task<IActionResult> AddItem(OrderCreateDTO addDTO)
        {
            var result = await _orderService.CreateOrder(addDTO);
            if (!result)
            {
                return BadRequest("Can not add!");
            }
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
    }
}
