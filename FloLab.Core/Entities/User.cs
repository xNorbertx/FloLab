using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FloLab.Core.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public int RoleId { get; set; }
        public int? SubscriptionId { get; set; }        
        public List<Reservation> Reservations { get; set; }
        public List<UserDay> UserDays { get; set; }
        public List<Attendance> Attendances { get; set; }
    }
}
