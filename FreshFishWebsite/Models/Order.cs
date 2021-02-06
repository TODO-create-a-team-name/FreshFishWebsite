using System;
using System.Collections.Generic;

namespace FreshFishWebsite.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
