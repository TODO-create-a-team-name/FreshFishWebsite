using FreshFishWebsite.Extensions;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels.PoolVM;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class PoolRepository : IPoolRepository
    {
        private readonly FreshFishDbContext _context;
        public PoolRepository(FreshFishDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pool> GetStoragePools(int storageId)
        {
            return _context.Pools.GetPoolsByStorage(storageId);
        }

        public IEnumerable<Pool> GetAll()
        {
            return _context.Pools;
        }

        public async Task<Pool> GetPoolByIdAsync(int poolId)
        {
            return await _context.Pools.GetPoolByIdAsync(poolId);
        }

        public async Task<Pool> GetPoolWithProductsAsync(int poolId)
        {
            return await _context.Pools.GetPoolWithProductsAsync(poolId);
        }
        public async Task AddAsync(Pool pool)
        {
            _context.Pools.Add(pool);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pool pool)
        {
            _context.Pools.Update(pool);
            await _context.SaveChangesAsync();
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pool = await GetPoolByIdAsync(id);
            if (pool != null)
            {
                _context.Pools.Remove(pool);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetProductsKgSum(Pool pool)
        {
            return pool.ProductsInPool.Sum(t => t.TotalProductQuantityKg);
        }

        public async Task<int> GetMaxAmountOfProductsInPool(int id)
        {
            var pool = await GetPoolByIdAsync(id);
            if(pool.RemainingSpaceForProducts == pool.MaxProductsKg)
            {
                return pool.MaxProductsKg;
            }
            else
            {
                return pool.RemainingSpaceForProducts;
            }
        }

        public async Task AddProductsToPool(ProductsForPoolViewModel model)
        {
            var pool = await GetPoolByIdAsync(model.PoolId);
            var product = await new ProductRepository(_context).GetProductByIdAsync(model.ProductId);

            if(product.QuantityKg != 0 && pool.RemainingSpaceForProducts != 0)
            {
                product.RemainingQuantityKg -= model.QuantityKg;
                pool.RemainingSpaceForProducts -= model.QuantityKg;
            }
            var productInPool = new ProductInPool
            {
                Product = product,
                Pool = pool,
                TotalProductQuantityKg = model.QuantityKg
            };

            await new ProductInPoolRepository(_context).AddAsync(productInPool);
            await new ProductRepository(_context).UpdateAsync(product);
            await new PoolRepository(_context).UpdateAsync(pool);

        }
    }
}
