using Application.Interfaces;
using Application.ViewModels.TourGuideDTO;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Get All tourGuides
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourGuideService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Get tourGuide by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _tourGuideService.GetById(id);
            return Ok(result);
        }


        /// <summary>
        /// Create tour guide
        /// </summary>
        [Authorize]
        [HttpPost("/api/TourGuide")]
        public async Task<IActionResult> AddTourGuide(TourGuideCreateDTO dto)
        {
            var result = await _tourGuideService.AddTourGuide(dto);
            if(result == null)
            {
                return BadRequest("Can not add new tour guide!");
            }
            return CreatedAtAction(nameof(GetById), new {id=result.Id},result);
        }
    }
}
