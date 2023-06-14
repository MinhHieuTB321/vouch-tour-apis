using Application.Interfaces;
using Application.ViewModels.Product;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        public ProductController(IProductService productService, IProductImageService productImageService)
        {
            _productService = productService;
            _productImageService = productImageService;

        }

        /// <summary>
        /// Delete product
        /// </summary>
        [Authorize(Roles =nameof(RoleEnums.Supplier))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result) return NoContent();
            else return BadRequest("Deleted Failed");
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [Authorize]
        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            if(result.Count() > 0) { return Ok(result); }
            else return BadRequest();
            
        }

        /// <summary>
        /// Update product
        /// </summary>
        [Authorize(Roles =nameof(RoleEnums.Supplier))]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDTO updatedItem)
        {
            var result = await _productService.UpdateProduct(updatedItem);
            if (result) return NoContent();
            else return BadRequest();
        }

        /// <summary>
        /// Create product
        /// </summary>
        [Authorize(Roles =nameof(RoleEnums.Supplier))]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDTO createProductDTO)
        {
            /*createProductDTO.File = formFile;*/
            var result = await _productService.CreateProduct(createProductDTO);
            if (result is not null)
            {
                result.Images = new List<ProductImage>();
                foreach (var image in createProductDTO.File)
                {
                    if (image is not null)
                    {
                        var createdImage = await _productImageService.AddImageAsync(image, result.Id);
                        if (createdImage is not null)
                        {
                            result.Images.Add(createdImage);
                        }
                        else throw new Exception("Create Image failed!");
                        
                    }
                    else throw new Exception("Image is null");


                }
                return CreatedAtAction(nameof(GetById), new {id = result.Id},result );
                    
            }
            else return BadRequest("Can not create Product");
        }


        /// <summary>
        /// Get product by Id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }
    }
}
