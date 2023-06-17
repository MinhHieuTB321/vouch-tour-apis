using Application.Interfaces;
using Application.ViewModels.CategoryDTO;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace WebAPI.Controllers
{
    [Route("/api/categories")]   
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
        [Authorize(Roles = nameof(RoleEnums.Admin))]
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
        [Authorize(Roles =nameof(RoleEnums.Admin))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _categoryService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// create category
        /// </summary>
        /// 
        [Authorize(Roles = nameof(RoleEnums.Admin))]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]CategoryCreateDTO createDTO)
        {
            var result = await _categoryService.Create(createDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// update category
        /// </summary>
        /// 
        [Authorize(Roles = nameof(RoleEnums.Admin))]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm]CategoryUpdateDTO updateDTO)
        {
            var result = await _categoryService.Update(updateDTO);
            return NoContent();
        }

        /// <summary>
        /// Delete category by Id
        /// </summary>
        /// 
        [Authorize(Roles = nameof(RoleEnums.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAsync(Guid id)
        {
            var result = await _categoryService.DeleteById(id);
            return Ok("Delete successfully!");
        }

        /// <summary>
        /// Get product by category
        /// </summary>
        /// 
        [Authorize]
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetPoductByCategoryId(Guid id)
        {
            var result = await _categoryService.GetProuductByCategoryId(id);
            return Ok(result);
        }
    }
}
