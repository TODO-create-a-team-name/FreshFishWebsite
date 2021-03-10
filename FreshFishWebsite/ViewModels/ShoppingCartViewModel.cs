using FreshFishWebsite.Models;
using System.Collections.Generic;

namespace FreshFishWebsite.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartProduct> Products { get; set; }

        public double TotalPrice { get; set; }

        public int Count { get; set; }
    }
}
