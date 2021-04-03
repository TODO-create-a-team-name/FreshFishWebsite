using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class ProductsDbContextExtensions
    {
        public static async Task<Product> GetProductByIdAsync(this DbSet<Product> products, int id)
        {
            return await products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public static IEnumerable<Product> GetProductsByStorageId(this DbSet<Product> products, int storageId)
        {
            return products.Where(s => s.StorageId == storageId);
        }
    }
}
