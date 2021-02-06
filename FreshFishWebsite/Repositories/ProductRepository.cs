
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private FreshFishDbContext _;
        public ProductRepository(FreshFishDbContext context)
        {
            _ = context;
        }
        public IEnumerable<Product> GetAll()
        => _.Products;

        public Product GetById(int? id)
        => _.Products.FirstOrDefault(p => p.Id == id);

        public async Task AddAsync(Product newItem)
        {
            await _.Products.AddAsync(newItem);
            await _.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            _.Products.Update(item);
            await _.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = GetById(id);
            if(product != null)
            {
                _.Products.Remove(product);
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
