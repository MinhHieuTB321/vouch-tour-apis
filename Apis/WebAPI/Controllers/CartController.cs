using Application.Interfaces;
using Application.Services;
using Application.ViewModels.CartDTO;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CartController:BaseController
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Get all items in cart
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cartService.GetAllItems();
            if (result == null)
            {
                return BadRequest("There is no product in cart!");
            }
            return Ok(result);
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet("{cartid}/items/{id}")]
        public async Task<IActionResult> GetById(string cartid,string id)
        {
            var result = await _cartService.GetItemById(cartid,id);
            if (result == null)
            {
                return BadRequest("There is no product in cart!");
            }
            return Ok(result);
        }

        /// <summary>
        /// Add item to cart
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPost]
        public async Task<IActionResult> AddItem(ItemAddDTO addDTO)
        {
            var result = await _cartService.AddToCart(addDTO);
            if (!result)
            {
                return BadRequest("Can not add!");
            }
            return Ok("Adding successfully!");
        }

        /// <summary>
        /// Update item in cart
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPut]
        public async Task<IActionResult> UpdateItem(ItemUpdateDTO  updateDTO)
        {
            var result = await _cartService.UpdateItem(updateDTO);
            if (!result)
            {
                return BadRequest("There is no product in cart!");
            }
            return Ok("Update Successfully");
        }


        /// <summary>
        /// Update item in cart
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpDelete("{cartid}/items/{id}")]
        public async Task<IActionResult> DeleteItem(string cartid, string id)
        {
            var result = await _cartService.DeleteItem(cartid,id);
            if (!result)
            {
                return BadRequest("There is no product in cart!");
            }
            return Ok("Delete Successfully!");
        }

        [HttpGet("DemoNoti")]
        public async Task<IActionResult> DemoNoti()
        {
            await _cartService.DemoNoti();
            return Ok();
        }
    }
}
