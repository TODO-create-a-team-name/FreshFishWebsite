using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.StorageByIdGetters
{
    public class StorageWithOrderItemsGetterById : GetterById<Storage>
    {
        private readonly int? _id;
        public StorageWithOrderItemsGetterById(int? id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }
        public override async Task<Storage> GetByIdAsync()
        {
            return await _context.Storages
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Order)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(x => x.Id == _id);
        }
    }
}
