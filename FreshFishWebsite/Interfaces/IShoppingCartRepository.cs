using FreshFishWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IShoppingCartRepository
    {
        public IEnumerable<ShoppingCartProduct> GetShoppingCartItems(User user);

        public Task AddProductToShoppingCart(User user, int productId);
    }
}
