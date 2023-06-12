using Application.Interfaces;
using Application.ViewModels.SupplierDTO;
using Domain.Enums;
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
        [Authorize(Roles = nameof(RoleEnums.Admin))]
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
        [Authorize(Roles = "Admin,Supplier")]
        [HttpGet("{id}")]
         public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _supplerService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Create Supplier
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.Admin))]
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
        /// <summary>
        /// Update Supplier
        /// </summary>
        [Authorize(Roles =nameof(RoleEnums.Supplier))]
        [HttpPut]
        public async Task<IActionResult> UpdateSupplier(SupplierUpdateDTO updateDTO)
        {
            var result = await _supplerService.Update(updateDTO);
            if (result)
            {
                return NoContent();
            }
            throw new Exception("Not Found!");
        }

        /// <summary>
        /// Soft remove supplier
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSupplier(Guid id)
        {
            var result= await _supplerService.Delete(id);
            if(result)
            {
                return Ok("Remove Successfully");
            }
            return BadRequest("Remove Fail!");
        }
    }
}
