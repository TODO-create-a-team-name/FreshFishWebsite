using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class PoolDbContextExtensions
    {
        public static IEnumerable<Pool> GetPoolsByStorage(this DbSet<Pool> pools, int storageId)
        {
            return pools.Where(p => p.StorageId == storageId);
        }
        public static async Task<Pool> GetPoolByIdAsync(this DbSet<Pool> pools, int poolId)
        {
            return await pools.FirstOrDefaultAsync(x => x.Id == poolId);
        }

        public static async Task<Pool> GetPoolWithProductsAsync(this DbSet<Pool> pools, int poolId) 
        {
            return await pools
                .Include(p => p.ProductsInPool)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == poolId);
        }

        public static async Task<Pool> GetPoolById(this DbSet<Pool> pools, int id)
        {
            return await pools.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
