using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly FreshFishDbContext _;
        public ShoppingCartRepository(FreshFishDbContext context)
        {
            _ = context;
        }

        public IEnumerable<ShoppingCartProduct> GetShoppingCartItems(User user)
        {
            var shoppingCart = _.ShoppingCarts.Include(p => p.Products).ThenInclude(p => p.Product).FirstOrDefault(u => u.User.Id == user.Id);
            return shoppingCart.Products;
        }

        public async Task AddProductToShoppingCart(User user, int productId)
        {
            var product = _.Products.FirstOrDefault(p => p.Id == productId);
            var shoppingCartProduct = new ShoppingCartProduct
            {
                Quantity = 1,
                Product = product
            };
            await _.ShoppingCartProducts.AddAsync(shoppingCartProduct);

            var shoppingCart = _.ShoppingCarts.FirstOrDefault(u => u.User.Id == user.Id);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    User = user
                };
                shoppingCart.Products.Add(shoppingCartProduct);
                await _.ShoppingCarts.AddAsync(shoppingCart);
            }
            else
            {
                shoppingCart.Products.Add(shoppingCartProduct);
                _.ShoppingCarts.Update(shoppingCart);
            }
            
            await _.SaveChangesAsync();
        }
    }
}
