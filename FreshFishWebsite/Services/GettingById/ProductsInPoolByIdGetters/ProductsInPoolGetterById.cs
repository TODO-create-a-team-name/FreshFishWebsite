using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.ProductsInPoolByIdGetters
{
    public class ProductsInPoolGetterById : GetterById<ProductInPool>
    {
        private readonly int? _id;
        public ProductsInPoolGetterById(int? id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }
        public override async Task<ProductInPool> GetByIdAsync()
        {
            return await _context.ProductsInPool.FirstOrDefaultAsync(x => x.Id == _id);
        }
    }
}
