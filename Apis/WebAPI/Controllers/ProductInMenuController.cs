using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //[Route("/api/product-menus")]
    //public class ProductInMenuController:BaseController
    //{
    //    private readonly IProductMenuService _productMenuService;
    //    public ProductInMenuController(IProductMenuService productMenuService)
    //    {
    //           _productMenuService=productMenuService;
    //    }


    //    /// <summary>
    //    /// Delete product in menu by id
    //    /// </summary>
    //    /// 
    //    [Authorize(Roles = nameof(RoleEnums.TourGuide))]
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> Delete(Guid id)
    //    {
    //        var result = await _productMenuService.DeleteProductFromMenu(id); 
    //        return Ok(result);
    //    }
    //}
}
