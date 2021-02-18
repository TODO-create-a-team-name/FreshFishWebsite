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

        public bool IsOrderAssigned { get; set; } = false;

        public List<Storage> Storages { get; set; } = new();

        public List<ShoppingCartProduct> Products { get; set; } = new();

    }
}
