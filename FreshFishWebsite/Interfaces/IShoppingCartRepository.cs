using FreshFishWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IShoppingCartRepository
    {
        public IEnumerable<ShoppingCartProduct> GetShoppingCartItems(User user);

        public Task<ShoppingCartProduct> GetById(int id);

        public Task AddProductToShoppingCart(User user, int productId);

        public Task OrderProducts(string id);

        public Task EditProductInShoppingCart(ShoppingCartProduct product);

        public Task DeleteProductInShoppingCart(int id);

        public Task IncrementDecrementQuantity(int id, int quantity);
    }
}
