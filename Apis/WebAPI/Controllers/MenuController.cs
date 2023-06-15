using Application.Interfaces;
using Application.Services;
using Application.ViewModels.ProductInMenuDTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Get menu by id of tour-guide
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet("{id}/products-menu")]
        public async Task<IActionResult> GetMenuById(Guid id)
        {
            var result = await _menuService.GetMenuViewAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// add product to menu
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPost("{id}/products")]
        public async Task<IActionResult> AddProductToMenu(Guid id,[FromBody]List<ProductMenuCreateDTO> products)
        {
            var result = await _menuService.AddProductToMenu(id,products);
            return CreatedAtAction(nameof(GetMenuById), new { id = result });
        }
    }
}
