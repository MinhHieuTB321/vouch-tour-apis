using Application.Interfaces;
using Application.Services;
using Application.ViewModels.GroupDTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    public class GroupController:BaseController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// Delete product
        /// </summary>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await _productService.DeleteProduct(id);
        //    if (result) return NoContent();
        //    else return BadRequest("Deleted Failed");
        //}

        /// <summary>
        /// Get all groups of tour-guide
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var result = await _groupService.GetAllGroupAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get groups by id 
        /// </summary>
        //[Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var result = await _groupService.GetGroupByIdAsyn(id);
            return Ok(result);
        }


        /// <summary>
        /// Create group
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GroupCreateDTO createDTO)
        {
            var result = await _groupService.CreateGroupAsync(createDTO);
            if (result==null) return BadRequest("Create fail!");
            
            return CreatedAtAction(nameof(GetGroupById), new {id=result.Id},result);
        }

        /// <summary>
        /// Update group
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(GroupUpdateDTO updateDTO)
        {
            var result = await _groupService.UpdateGroupAsync(updateDTO);
            if (result) return NoContent();

            return BadRequest("Update fail!");
        }

        /// <summary>
        /// Get all orders in group by groupid
        /// </summary>
        [Authorize(Roles = nameof(RoleEnums.TourGuide))]
        [HttpGet("{groupid}/orders")]
        public async Task<IActionResult> GetOrderByGroupId(Guid groupid)
        {
            var result = await _groupService.GetAllOrdersAsync(groupid);
            return Ok(result);
        }
    }
}
