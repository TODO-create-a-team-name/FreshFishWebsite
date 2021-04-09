using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IShoppingCartRepository
    {
        public IEnumerable<ShoppingCartProduct> GetShoppingCartItems(string userId);

        public Task<ShoppingCartProduct> GetById(int id);

        public Task AddProductToShoppingCart(User user, int productId);

        public Task OrderProducts(string id);

        public Task EditProductInShoppingCart(ShoppingCartProduct product);

        public Task DeleteProductInShoppingCart(int id);

        public Task ChangeQuantity(int id, int quantity);

        public JsonResult GetProductsDataJson();
    }
}
