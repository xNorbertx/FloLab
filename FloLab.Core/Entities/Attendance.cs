using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FloLab.Core.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
