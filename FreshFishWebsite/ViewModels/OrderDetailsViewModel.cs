using FreshFishWebsite.Models;
using System.Collections;
using System.Collections.Generic;

namespace FreshFishWebsite.ViewModels
{
    public class OrderDetailsViewModel
    {
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }


        public IEnumerable<ShoppingCartProduct> Products { get; set; }
    }
}
