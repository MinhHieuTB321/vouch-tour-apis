using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebAPI.Controllers
{
    public class TourGuideController : BaseController
    {
        private readonly ITourGuideService _tourGuideService;
        public TourGuideController(ITourGuideService tourGuideService)
        {
               _tourGuideService = tourGuideService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourGuideService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _tourGuideService.GetById(id);
            return Ok(result);
        }
    }
}
