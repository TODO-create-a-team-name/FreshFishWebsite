
using System;
using System.Collections.Generic;

namespace FreshFishWebsite.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public bool IsSold { get; set; } = false;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
