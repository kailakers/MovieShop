using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUserAsync([FromBody] UserRegisterRequestModel user)
        {
            if (user is null) return BadRequest();

            var createdUser = await _userService.CreateUser(user);
            if (createdUser == null) return BadRequest(" Username Already Exists!");

            return Ok(createdUser);
            // return CreatedAtAction(nameof(GetUserByIdAsync), new { id = createdUser.Id }, null);
        }
    }
}