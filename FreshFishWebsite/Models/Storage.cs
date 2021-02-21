using System.Collections.Generic;

namespace FreshFishWebsite.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Address { get; set; } 
        public int StorageNumber { get; set; }
        public string AdminId { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<User> Workers { get; set; } = new();
        public List<OrderItems> OrderItems { get; set; } = new();
    }
}
