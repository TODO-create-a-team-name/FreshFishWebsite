using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.PoolByIdGetters
{
    public class PoolGetterById : GetterById<Pool>
    {
        private readonly int _id;
        public PoolGetterById(int id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }
        public override async Task<Pool> GetByIdAsync()
        {
            return await _context.Pools.FirstOrDefaultAsync(x => x.Id == _id);
        }
    }
}
