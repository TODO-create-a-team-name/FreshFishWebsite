using FreshFishWebsite.Extensions;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class ProductInPoolRepository : IProductInPoolRepository
    {
        private readonly FreshFishDbContext _context;
        public ProductInPoolRepository(FreshFishDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductInPool> GetAll()
            => _context.ProductsInPool
            .Include(p => p.Product);

        public async Task<ProductInPool> GetProductInPoolById(int productInPoolId)
        {
            return await _context.ProductsInPool.GetProductInPoolByIdAsync(productInPoolId);
        }

        public async Task<ProductInPool> GetProdyctInPoolWithPoolById(int productInPoolId)
        {
            return await _context.ProductsInPool.GetProductInPoolWithPoolByIdAsync(productInPoolId);
        }

        public async Task AddAsync(ProductInPool productInPool)
        {
            _context.ProductsInPool.Add(productInPool);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ProductInPool productInPool)
        {
            _context.ProductsInPool.Update(productInPool);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productInPool = await GetProdyctInPoolWithPoolById(id);
            if (productInPool != null)
            {
                 ReturnPoolCapacityOfProductInPoolSize(productInPool);
                _context.ProductsInPool.Remove(productInPool);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ReturnPoolCapacityOfProductInPoolSize(ProductInPool productInPool)
        {
            var quantity = productInPool.TotalProductQuantityKg;

            productInPool.Pool.RemainingSpaceForProducts += quantity;
            _context.Pools.Update(productInPool.Pool);

            productInPool.Product.RemainingQuantityKg += quantity;
            _context.Products.Update(productInPool.Product);
        }
    }
}
