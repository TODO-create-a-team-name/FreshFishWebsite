using FreshFishWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.ViewModels.PoolVM
{
    public class ProductsForPoolViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public int QuantityKg { get; set; }
        public int PoolId { get; set; }
        public Pool Pool { get; set; }
    }
}
