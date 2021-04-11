using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using FreshFishWebsite.ViewModels.PoolVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IPoolRepository : ICrud<Pool>
    {
        public IEnumerable<Pool> GetStoragePools(int storageId);
        public Task<Pool> GetPoolByIdAsync(int poolId);
        public Task<Pool> GetPoolWithProductsAsync(int poolId);
        public int GetProductsKgSum(Pool pool);
        public Task<int> GetMaxAmountOfProductsInPool(int id);
        public Task AddProductsToPool(ProductsForPoolViewModel model);
        public Task<Pool> ChangeRemainingSpaceForProducts(int poolId, int maxProductsKg);
        public Task AddFeedInfo(FeedFishViewModel model);
    }
}
