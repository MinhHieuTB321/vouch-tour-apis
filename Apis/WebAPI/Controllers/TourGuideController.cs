using Application.Interfaces;
using Application.Services;
using Application.ViewModels.TourGuideDTO;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebAPI.Controllers
{
    [Route("api/tour-guides")]
    public class TourGuideController : BaseController
    {
        private readonly ITourGuideService _tourGuideService;
        public TourGuideController(ITourGuideService tourGuideService)
        {
               _tourGuideService = tourGuideService;
        }

        #region GET

        /// <summary>
        /// Get All tourGuides
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourGuideService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Get tourGuide by Id
        /// </summary>
        [Authorize(Roles = "Admin,TourGuide")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _tourGuideService.GetById(id);
            return Ok(result);
        }

        #endregion


        #region POST

        /// <summary>
        /// Create tour guide
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.Admin))]
        [HttpPost]
        public async Task<IActionResult> AddTourGuide(TourGuideCreateDTO dto)
        {
            var result = await _tourGuideService.AddTourGuide(dto);
            if (result == null)
            {
                return BadRequest("Can not add new tour guide!");
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        #endregion

        #region PUT

        /// <summary>
        /// Update tour-guide
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPut]
        public async Task<IActionResult> UpdateTourGuide(TourGuideUpdateDTO dto)
        {
            var result = await _tourGuideService.UpdateTourGuideAsync(dto);
            if (result)
            {
                return NoContent();
            }
            throw new Exception("Not Found!");
        }


        #endregion


        #region DELETE

        /// <summary>
        /// Soft remove tour-guide
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSupplier(Guid id)
        {
            var result = await _tourGuideService.DeleteTourGuideAsync(id);
            if (result)
            {
                return Ok("Remove Successfully");
            }
            return BadRequest("Remove Fail!");
        }

        #endregion
    }
}
