using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    public class UserController:BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> DemoApi()
        {
            var result = await _userService.GetAllUsers();
            if (result ==null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

    }
}
