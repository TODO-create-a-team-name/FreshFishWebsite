using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.StorageByIdGetters
{
    public class StorageWithWorkersGetterById : GetterById<Storage>
    {
        private readonly int? _id;
        public StorageWithWorkersGetterById(int? id, FreshFishDbContext context) : base(context) 
        {
            _id = id;
        }
        public override async Task<Storage> GetByIdAsync()
        {
            return await _context
                .Storages
                .Include(d => d.Drivers)
                .FirstOrDefaultAsync(x => x.Id == _id);
        }
    }
}
