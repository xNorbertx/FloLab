using FloLab.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FloLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getdays", Name = "GetUserDays")]
        public IActionResult GetUserDays([FromQuery(Name = "UserId")]string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            IList<int> userDays = _userService.GetUserDays(userId);

            return Ok(userDays);
        }
    }
}
