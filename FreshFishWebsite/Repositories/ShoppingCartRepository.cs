using FreshFishWebsite.Extensions;
using FreshFishWebsite.Helpers;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<ShoppingCartProduct> GetShoppingCartItems(string userId)
        {
            var shoppingCart = _context.ShoppingCarts
                .GetShoppingCartWithProductsByUserId(userId);
                
            return shoppingCart?.Products;
        }

        public async Task<ShoppingCartProduct> GetById(int id) => 
            await _context.ShoppingCartProducts.GetShoppingCartProductWithProductById(id);

        public async Task AddProductToShoppingCart(User user, int productId)
        {
            var products = GetShoppingCartItems(user.Id);
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

            await SaveContextChangesAsync();
        }


        public async Task EditProductInShoppingCart(ShoppingCartProduct product)
        {
            _context.ShoppingCartProducts.Update(product);

            await SaveContextChangesAsync();
        }

        public async Task OrderProducts(string id)
        {
            var user = await _context.Users.GetUserWithOrdersAndShoppingCart(id);

            var order = new Order
            {
                Products = user.ShoppingCart.Products
            };

            await AddOrderAsync(user, order);

            //await new EmailService().SendEmailAsync("freshfishofficial@gmail.com", "Нове замовлення",
            //           $"Нове замовлення від {user.Email}");
            await EmailMessageSender.SendNewOrderMessageAsync(user.Email);
        }

        public async Task DeleteProductInShoppingCart(int id)
        {
            var product = await _context
                .ShoppingCartProducts
                .GetShoppingCartProductById(id);

            _context.ShoppingCartProducts.Remove(product);

            await SaveContextChangesAsync();
        }

        private async Task AddToShoppingCart(User user, int productId)
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

        public async Task ChangeQuantity(int id, int quantity)
        {
            var product = await _context.ShoppingCartProducts.FirstOrDefaultAsync(x => x.Id == id);

            product.Quantity = quantity;

            _context.ShoppingCartProducts.Update(product);

            await SaveContextChangesAsync();

        }

        private async Task AddOrderAsync(User user, Order order)
        {
            await _context.Orders.AddAsync(order);

            order.Products.ForEach(p => p.ShoppingCart = null);

            user.Orders.Add(order);

            _context.Users.Update(user);

            await SaveContextChangesAsync();
        }

        private async Task SaveContextChangesAsync()
        {
            _ = await _context.SaveChangesAsync();
        }

        public JsonResult GetProductsDataJson()
        {
            return new JsonResult(_context
            .Products
            .Select(p => new { p.Id, p.ProductName, p.PricePerKg, p.Date, p.Image, p.Description, p.Calories }));
        }
    }
}
