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
    public class ReservationController : ControllerBase
    {
        public IRepository<Reservation> _repository;

        public ReservationController(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult GetUserReservations([FromQuery(Name = "UserId")]string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            //Eventueel checken of gebruiker bestaat

            IList<Reservation> reservations = _repository.Filter(x => x.UserId == userId).ToList();

            return Ok(reservations);
        }

    }
}
