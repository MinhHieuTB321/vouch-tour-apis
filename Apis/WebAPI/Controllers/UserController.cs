﻿using Application.Interfaces;
using Application.ViewModels.UserDTOs;
using Microsoft.AspNetCore.Authorization;
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


        /// <summary>
        /// Get All Users
        /// </summary>
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


        /// <summary>
        /// Get authenticate token
        /// </summary>
        [HttpPost("/api/authentication")]
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
        {
            var authToken = await _userService.LoginAsync(loginDTO);
            //_logger.LogInformation("Auth Token: " + authToken);
            if (authToken == null)
            {
                return BadRequest("Login Fail!");
            }
            return Ok(authToken);
        }


    }
}
