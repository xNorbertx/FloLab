using System;
using System.ComponentModel.DataAnnotations;

namespace FloLab.Core.DTOs
{
    public class BookingDTO
    {
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public string UserId { get; set; }
        public int? SpaceId { get; set; }
    }
}
