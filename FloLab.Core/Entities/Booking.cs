using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FloLab.Core.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [Required]
        public string UserId { get; set; }
        public int SpaceId { get; set; }
    }
}
