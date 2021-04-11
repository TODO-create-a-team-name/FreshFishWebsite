using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FreshFishWebsite.Models
{
    public class Pool
    {
        public int Id { get; set; }
        public int PoolNumber { get; set; }
        public int MaxProductsKg { get; set; }
        public int RemainingSpaceForProducts { get; set; }
        public DateTime DateTimeFeedingExpired { get; set; }
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

        private bool? _isFishFed;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool? IsFishFed
        {
            get
            {
                var currentDate = DateTime.Now; // DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                _isFishFed = DateTime.Compare(DateTimeFeedingExpired, currentDate) > 0;
                return _isFishFed;
            }
            private set{}
        } 

    }
}
