using FloLab.Core.DTOs;
using FloLab.Core.Entities;
using FloLab.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _userService;

        public AuthController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            Microsoft.AspNetCore.Identity.SignInResult res = await _userService.Login(model);

            User user = await _userService.FindUserByEmail(model.email);

            if (res.Succeeded)
            {
                var token = await _userService.Authenticate(user);

                return Ok(new { token });
            }

            if (user == null)
            {
                return BadRequest("InvalidUserNameOrPassword");
            }

            return BadRequest("LoginFailed");
        }
    }
}
