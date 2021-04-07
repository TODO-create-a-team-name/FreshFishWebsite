using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class OrderDbContextExtensions
    {
        public static IEnumerable<Order> GetOrdersWithUsers(this DbSet<Order> orders)
        {
            return orders.Include(u => u.User).AsNoTracking();
        }

        public static async Task<Order> GetOrderById(this DbSet<Order> orders, int orderId)
        {
            return await orders.FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public static async Task<Order> GetOrderWithUserAndProductsWithStorages(this DbSet<Order> orders, int orderId)
        {
            return await orders
                .Include(u => u.User)
                .Include(p => p.Products)
                .ThenInclude(p => p.Product)
                .ThenInclude(s => s.Storage)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }
    }
}
