using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class ProductsInPoolDbContextExtensions
    {
        public static async Task<ProductInPool> GetProductInPoolByIdAsync(this DbSet<ProductInPool> productsInPool, int id)
        {
            return await productsInPool.FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<ProductInPool> GetProductInPoolWithPoolByIdAsync(this DbSet<ProductInPool> productsInPool, int id)
        {
            return await productsInPool
                .Include(p => p.Product)
                .Include(p => p.Pool)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
