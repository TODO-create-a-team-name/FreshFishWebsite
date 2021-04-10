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
        public bool IsFishFed { get; set; } = false;
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public List<ProductInPool> ProductsInPool { get; set; } = new();
        public List<PoolState> PoolStates { get; set; } = new();
        public List<Feeding> Feeding { get; set; } = new();
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int FishQuantly
        {
            get { return MaxProductsKg - RemainingSpaceForProducts; }
            private set {}
        }

    }
}
