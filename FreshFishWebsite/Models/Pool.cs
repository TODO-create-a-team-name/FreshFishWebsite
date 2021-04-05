using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreshFishWebsite.Models
{
    public class Pool
    {
        public int Id { get; set; }
        public int PoolNumber { get; set; }
        public int MaxProductsKg { get; set; }
        public int RemainingSpaceForProducts { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int FishQuantly
        {
            get { return MaxProductsKg - RemainingSpaceForProducts; }
            private set {}
        }
        public double WaterTemperatureCelsius { get; set; }
        public double NitrogenLevel { get; set; }
        public bool IsFishFed { get; set; } = false;
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public List<ProductInPool> ProductsInPool { get; set; } = new();
        public List<Feeding> Feeding { get; set; } = new();

    }
}
