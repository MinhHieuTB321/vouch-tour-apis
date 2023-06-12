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




        /// <summary>
        /// Get all categories
        /// </summary>
        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return Ok(result);
           
        }

        /// <summary>
        /// Get category by Id
        /// </summary>
        /// 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _categoryService.GetById(id);
            return Ok(result);
        }
    }
}
