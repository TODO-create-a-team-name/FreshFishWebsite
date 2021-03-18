
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace FreshFishWebsite.ViewModels
{
    public class RolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
