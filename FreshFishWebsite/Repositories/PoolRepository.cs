﻿using FreshFishWebsite.Extensions;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels.PoolVM;
using System;
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

        public async Task<Pool> ChangeRemainingSpaceForProducts(int poolId, int maxProductsKg)
        {
            var pool = await GetPoolWithProductsAsync(poolId);

            if(pool.MaxProductsKg == maxProductsKg)
            {
                return pool;
            }
            else
            {
                pool.MaxProductsKg = maxProductsKg;
                var productsQuantityInPool = pool.ProductsInPool.Sum(x => x.TotalProductQuantityKg);
                var remainingSpaceForProducts = maxProductsKg - productsQuantityInPool;
                if(remainingSpaceForProducts < 0)
                {
                    pool.RemainingSpaceForProducts = 0;
                }
                else
                {
                    pool.RemainingSpaceForProducts = remainingSpaceForProducts;
                }
                return pool;
            }
        }

        public async Task AddFeedInfo(FeedFishViewModel model)
        {
            await UpdatePoolExpireFeedingDate(model);
            await AddFeedAsync(model);
            await AddPoolStateAsync(model);
            await _context.SaveChangesAsync();
        }

        private async Task AddFeedAsync(FeedFishViewModel model)
        {
            Feeding feed = new()
            {
                Name = model.FeedName,
                DateTimeFed = DateTime.Now,
                PoolId = model.PoolId,

            };

            await _context.Feedings.AddAsync(feed);
        }

        private async Task AddPoolStateAsync(FeedFishViewModel model)
        {
            PoolState poolState = new()
            {
                Temperature = model.Temperature,
                Nitrogen = model.Nitrogen,
                WaterLevel = model.WaterLevel,
                PoolId = model.PoolId
            };
            await _context.PoolStates.AddAsync(poolState);
        }
        private async Task UpdatePoolExpireFeedingDate(FeedFishViewModel model)
        {
            var pool = await _context.Pools.GetPoolById(model.PoolId);
            var expireFeedDate = DateTime.Parse(model.ExpireFeedDate.ToString("MM/dd/yyyy HH:mm:ss"));
            pool.DateTimeFeedingExpired = expireFeedDate;
            await UpdateAsync(pool);
        }

        public IEnumerable<PoolState> GetPoolStatesData(int poolId)
        {
            return _context.PoolStates.GetPoolStatesByPoolId(poolId);
        }
    }
}
