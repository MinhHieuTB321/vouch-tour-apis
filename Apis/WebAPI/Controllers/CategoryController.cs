using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace WebAPI.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return Ok(result);
           
        }
    }
}
