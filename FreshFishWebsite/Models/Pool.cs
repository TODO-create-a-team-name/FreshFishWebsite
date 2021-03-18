using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Collections.Generic;

namespace FreshFishWebsite.Models
{
    public class Pool
    {
        public int Id { get; set; }
        public int PoolNumber { get; set; }
        public int MaxProductsKg { get; set; }
        public double WaterTemperatureCelsius { get; set; }
        public double NitrogenLevel { get; set; }
        public bool IsFishFed { get; set; } = false;
        public List<ProductInPool> ProductsInPool { get; set; } = new();
        public int StorageId { get; set; }
        public Storage Storage { get; set; }

    }
}
