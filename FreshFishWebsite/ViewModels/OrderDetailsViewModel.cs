using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FreshFishWebsite.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int OrderItemsId { get; set; }
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string DriverId { get; set; }

        public List<SelectListItem> Drivers { get; set; }

        public IEnumerable<ShoppingCartProduct> Products { get; set; }
    }
}
