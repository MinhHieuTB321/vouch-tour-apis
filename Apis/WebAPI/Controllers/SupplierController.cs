using Application.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _supplerService.GetAll();
            if (result.Count() > 0) return Ok(result);
            else return BadRequest("Not have any Supplier");
        }
    }
}
