using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreshFishWebsite.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Usersurname { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string CompanyAddress { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
