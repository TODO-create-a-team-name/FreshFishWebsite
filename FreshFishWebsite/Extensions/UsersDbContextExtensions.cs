using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class UsersDbContextExtensions
    {
        public static async Task<User> GetUserWithOrdersAndShoppingCart(this DbSet<User> users, string userId)
        {
            return await users
                .Include(o => o.Orders)
                .ThenInclude(p => p.Products)
                .Include(s => s.ShoppingCart)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
