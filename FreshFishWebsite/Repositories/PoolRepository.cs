using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class PoolRepository : IPoolRepository
    {
        private readonly FreshFishDbContext _;
        public PoolRepository(FreshFishDbContext context)
        {
            _ = context;
        }
        public IEnumerable<Pool> GetAll()
        {
            return _.Pools;
        }
        public IEnumerable<Pool> GetStoragePools(int id)
        {
            return _.Pools.Where(p => p.StorageId == id);
        }

        public Pool GetById(int? id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public async Task AddAsync(Pool pool)
        {
            _.Pools.Add(pool);
            await _.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pool pool)
        {
            _.Pools.Update(pool);
            await _.SaveChangesAsync();
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pool = GetById(id);
            if (pool != null)
            {
                _.Pools.Remove(pool);
                await _.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
