using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CustomerController:BaseController
    {
        private readonly IOrderService _orderService;
        public CustomerController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        /// <summary>
        /// Get order by phone number
        /// </summary>
        [HttpGet("{phonenumber}/orders")]
        public async Task<IActionResult> GetOrderByPhone(string phonenumber)
        {
            var result = await _orderService.GetOrderByPhone(phonenumber);
            return Ok(result);
        }
    }
}
