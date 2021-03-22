using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.PoolByIdGetters
{
    public class PoolWithProductsGetterById : GetterById<Pool>
    {
        private readonly int _id;
        public PoolWithProductsGetterById(int id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }
        public override async Task<Pool> GetByIdAsync()
        {
            return await _context.Pools
                .Include(p => p.ProductsInPool)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == _id);
        }
    }
}
