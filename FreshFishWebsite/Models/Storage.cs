using System.Collections.Generic;

namespace FreshFishWebsite.Models
{
    public class Storage
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public User SrorageAdmin { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<User> Drivers { get; set; } = new List<User>();
    }
}
