﻿
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshFishWebsite.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double PricePerKg { get; set; }
        //public int Weight { get; set; }
        public DateTime Date { get; set; }
        public int QuantityKg { get; set; }
        public int RemainingQuantityKg { get; set; }
        public bool IsSold { get; set; } = false;
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new();
        public List<ProductInPool> ProductsInPool { get; set; } = new();
    }
}
