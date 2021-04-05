using FreshFishWebsite.Models;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IProductInPoolRepository : ICrud<ProductInPool>
    {
        public Task<ProductInPool> GetProductInPoolById(int productInPoolId);
        public Task<ProductInPool> GetProdyctInPoolWithPoolById(int productInPoolId);
    }
}
