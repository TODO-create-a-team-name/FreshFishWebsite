using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.StorageByIdGetters
{
    public class StorageGetterByAdminId : GetterById<Storage>
    {
        private readonly string _id;
        public StorageGetterByAdminId(string id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }

        public async override Task<Storage> GetByIdAsync()
        {
            return await _context.Storages
            .Include(p => p.Products)
            .Include(w => w.Drivers)
            .FirstOrDefaultAsync(x => x.StorageAdminId == _id);
        }
    }
}
