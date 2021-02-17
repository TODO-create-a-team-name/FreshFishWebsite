
using System.Collections.Generic;
namespace FreshFishWebsite.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<ShoppingCartProduct> Products { get; set; } = new List<ShoppingCartProduct>();
    }
}
