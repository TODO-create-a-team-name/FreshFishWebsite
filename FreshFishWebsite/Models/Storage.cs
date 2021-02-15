using System.Collections.Generic;

namespace FreshFishWebsite.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Address { get; set; } 
        public string AdminId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<User> Workers { get; set; } = new List<User>();
    }
}
