using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class ShoppingCartsDbContextExtensions
    {
        public static ShoppingCart GetShoppingCartWithProductsByUserId(this DbSet<ShoppingCart> shoppingCarts, string userId)
        {
            return shoppingCarts
                .Include(p => p.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(u => u.User.Id == userId);
        }
    }
}
