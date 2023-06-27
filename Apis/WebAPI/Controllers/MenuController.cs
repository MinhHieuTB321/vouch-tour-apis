using Application.Interfaces;
using Application.Services;
using Application.ViewModels.MenuDTOs;
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
        /// Get product in menu
        /// </summary>
        //[Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet("{id}/products-menu")]
        public async Task<IActionResult> GetProductInMenu(Guid id)
        {
            var result = await _menuService.GetProductInMenuViewAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// add product to menu
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPost("{id}/products")]
        public async Task<IActionResult> AddProductToMenu(Guid id,[FromBody]List<ProductMenuCreateDTO> products)
        {
            var result = await _menuService.AddListProductToMenu(id,products);
            return CreatedAtAction(nameof(GetMenuById), new { id = result },null);
        }

        /// <summary>
        /// Get menu by id
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(Guid id)
        {
            var result = await _menuService.GetMenuViewById(id);
            return Ok(result);
        }

        /// <summary>
        /// create menu
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPost]
        public async Task<IActionResult> CreateMenu(MenuCreateDTO createDTO)
        {
            var result = await _menuService.CreateMenu(createDTO);
            return CreatedAtAction(nameof(GetMenuById), new { id = result }, null);
        }

        /// <summary>
        /// Get all menu of tourguide
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet]
        public async Task<IActionResult> GetAllMenu()
        {
            var result = await _menuService.GetAllMenu();
            return Ok(result);
        }

        /// <summary>
        /// Delete menu
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpDelete("{menuid}")]
        public async Task<IActionResult> DeleteMenu(Guid menuid)
        {
            var result = await _menuService.DeleteMenu(menuid);
            return Ok("Delete successfully");
        }


        /// <summary>
        /// Update menu
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPut]
        public async Task<IActionResult> UpdateMenu(MenuUdpateDTO udpateDTO)
        {
            await _menuService.UpdateMenu(udpateDTO);
            return Ok("Update successfully");
        }

        /// <summary>
        ///Get product in menu by id
        /// </summary>
        [HttpGet("{menuid}/products-menu/{productid}")]
        public async Task<IActionResult> GetProductInMenuById(Guid menuid,Guid productid)
        {
            var result= await _menuService.GetProductInMenuById(menuid, productid);
            return Ok(result);
        }

        /// <summary>
        /// Delete product in menu by id
        /// </summary>
        /// 
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpDelete("{menuid}/products-menu/{productid}")]
        public async Task<IActionResult> Delete(Guid menuid,Guid productid)
        {
            var result = await _menuService.DeleteProductFromMenu(menuid,productid);
            return Ok(result);
        }

    }
}
