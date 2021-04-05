using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class OrderItemsDbContextExtensions
    {
        public static async Task<OrderItems> GetOrderItemsByIdAsync(this DbSet<OrderItems> orderItems, int orderItemsId)
        {
            return await orderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Include(o => o.Order)
            .ThenInclude(p => p.Products)
            .FirstOrDefaultAsync(x => x.Id == orderItemsId);
        }

        public static async Task<OrderItems> GetOrderItemsWithProductsByIdAsync(this DbSet<OrderItems> orderItems, int orderItemsId, int storageId)
        {
            return await orderItems
           .Include(o => o.Order)
           .ThenInclude(u => u.User)
           .Include(o => o.Order)
           .ThenInclude(p => p.Products.Where(p => p.Product.StorageId == storageId))
           .ThenInclude(p => p.Product)
           .FirstOrDefaultAsync(x => x.Id == orderItemsId && x.StorageId == storageId);
        }
    }
}
