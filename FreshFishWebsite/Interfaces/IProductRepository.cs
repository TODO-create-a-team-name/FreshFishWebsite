using FreshFishWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IProductRepository : ICrud<Product>
    {
        public Task<Product> GetProductByIdAsync(int id);
        public IEnumerable<Product> GetProductsByStorageId(int storageId);
    }
}
