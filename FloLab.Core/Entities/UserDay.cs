using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FloLab.Core.Entities
{
    public class UserDay
    {
        [Key]
        public int Id { get; set; }

        public int DayNumber { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
