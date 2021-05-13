using FloLab.Core.DTOs;
using FloLab.Core.Entities;
using FloLab.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FloLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public IRepository<Booking> _repository;

        public BookingController(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult GetBookings([FromQuery(Name = "Year")]int year, [FromQuery(Name = "Week")]int week)
        {
            //Eventueel checken of gebruiker bestaat

            DateTime weekStart = CalculateDateTimeFromWeek(year, week);

            IList<Booking> bookings = _repository.Filter(x => x.Start > weekStart &&
                                                              x.End < weekStart.AddDays(7)).ToList();

            return Ok(bookings);
        }

        [HttpPost]
        public IActionResult AddBooking([FromBody]BookingDTO model)
        {
            if (String.IsNullOrEmpty(model.UserId))
            {
                return BadRequest("User not found");
            }

            Booking booking = new Booking
            {
                Start = model.Start,
                End = model.End,
                UserId = model.UserId,
                SpaceId = (int)model.SpaceId
            };

            _repository.Insert(booking);

            return Ok(new ReturnMessageDTO { Message = "Booking completed" });
        }

        [HttpDelete]
        public IActionResult DeleteBooking(int bookingId)
        {
            if (!_repository.Exists(bookingId))
            {
                return BadRequest(new ReturnMessageDTO { Message = "Booking not found" });
            }

            Booking booking = _repository.Get(bookingId);

            _repository.Delete(booking);

            return Ok(new ReturnMessageDTO { Message = "Booking deleted" });
        }

        private static DateTime CalculateDateTimeFromWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
    }
}
