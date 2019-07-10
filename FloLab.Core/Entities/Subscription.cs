using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FloLab.Core.Entities
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
