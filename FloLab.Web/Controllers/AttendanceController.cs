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
    public class AttendanceController : ControllerBase
    {
        private IRepository<Attendance> _repository;

        public AttendanceController(IRepository<Attendance> repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult GetUserAttendances([FromQuery(Name = "UserId")]string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            //Eventueel checken of gebruiker bestaat

            IList<Attendance> attendances = _repository.Filter(x => x.UserId == userId).ToList();

            return Ok(attendances);
        }
    }
}
