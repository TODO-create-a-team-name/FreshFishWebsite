using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels.PoolVM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IPoolRepository : ICrud<Pool>, IGetterById
    {
        public int GetProductsKgSum(Pool pool);
        public Task<int> GetMaxAmountOfProductsInPool(int id);

        public Task AddProductsToPool(ProductsForPoolViewModel model);
    }
}
