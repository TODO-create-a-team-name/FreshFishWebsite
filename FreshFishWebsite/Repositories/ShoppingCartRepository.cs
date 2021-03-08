using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly FreshFishDbContext _context;
        public ShoppingCartRepository(FreshFishDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingCartProduct> GetShoppingCartItems(User user)
        {
            var shoppingCart = _context.ShoppingCarts
                .Include(p => p.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(u => u.User.Id == user.Id);

            return shoppingCart?.Products ?? null;
        }

        public async Task<ShoppingCartProduct> GetById(int id)
        => await _context.ShoppingCartProducts
            .Include(p => p.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddProductToShoppingCart(User user, int productId)
        {

            var products = GetShoppingCartItems(user);
            if (products!= null)
            {
                var inSoppingCard = products.FirstOrDefault(p => p.ProductId == productId);
                if (inSoppingCard != null)
                {
                    inSoppingCard.Quantity += 1;
                }
                else
                {
                    await AddToShoppingCart(user, productId);
                }
            }
            else 
                await AddToShoppingCart(user, productId);

            await _context.SaveChangesAsync();
        }


        public async Task EditProductInShoppingCart(ShoppingCartProduct product)
        {
            _context.ShoppingCartProducts.Update(product);

            await _context.SaveChangesAsync();
        }

        public async Task OrderProducts(string id)
        {
            var user = await _context.Users
                .Include(o => o.Orders)
                .ThenInclude(p => p.Products)
                .Include(s => s.ShoppingCart)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            var order = new Order
            {
                Products = user.ShoppingCart.Products
            };

            await _context.Orders.AddAsync(order);

            foreach (var p in user.ShoppingCart.Products)
            {
                p.ShoppingCart = null;
            }

            user.Orders.Add(order);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            await new EmailService().SendEmailAsync("freshfishofficial@gmail.com", "Нове замовлення",
                       $"Нове замовлення від {user.Email}");

        }

        public async Task DeleteProductInShoppingCart(int id)
        {
            var product = await _context.ShoppingCartProducts.FirstOrDefaultAsync(x => x.Id == id);

            _context.ShoppingCartProducts.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task AddToShoppingCart(User user, int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            var shoppingCartProduct = new ShoppingCartProduct
            {
                Quantity = 1,
                Product = product
            };
            await _context.ShoppingCartProducts.AddAsync(shoppingCartProduct);

            var shoppingCart = _context.ShoppingCarts.FirstOrDefault(u => u.User.Id == user.Id);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    User = user
                };
                shoppingCart.Products.Add(shoppingCartProduct);
                await _context.ShoppingCarts.AddAsync(shoppingCart);
            }
            else
            {
                shoppingCart.Products.Add(shoppingCartProduct);
                _context.ShoppingCarts.Update(shoppingCart);
            }
        }
    }
}
