using Application.Interfaces;
using Application.ViewModels.SupplierDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplerService;

        public SupplierController(ISupplierService supplerService)
        {
            _supplerService = supplerService;
        }


        /// <summary>
        /// Get all suppliers
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _supplerService.GetAll();
            if (result.Count() > 0) return Ok(result);
            else return BadRequest("Not have any Supplier");
        }


        /// <summary>
        /// Get supplier by Id
        /// </summary>
        [HttpGet("{id}")]
         public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _supplerService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Create Supplier
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser( SupplierCreateDTO createDTO)
        {
            var result = await _supplerService.Create(createDTO);
            if (result == null)
            {
                return BadRequest("Can not add " + createDTO.Email + " into Db"!);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}
