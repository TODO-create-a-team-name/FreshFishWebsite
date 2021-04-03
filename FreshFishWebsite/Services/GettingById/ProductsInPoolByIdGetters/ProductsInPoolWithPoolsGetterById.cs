using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.ProductsInPoolByIdGetters
{
    public class ProductsInPoolWithPoolsGetterById : GetterById<ProductInPool>
    {
        private readonly int? _id;
        public ProductsInPoolWithPoolsGetterById(int? id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }
        public override async Task<ProductInPool> GetByIdAsync()
        {
            return await _context.ProductsInPool
                .Include(p => p.Product)
                .Include(p => p.Pool)
                .FirstOrDefaultAsync(x => x.Id == _id);
        }
    }
}
