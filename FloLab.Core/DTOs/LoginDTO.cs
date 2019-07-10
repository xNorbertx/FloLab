using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FloLab.Core.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
