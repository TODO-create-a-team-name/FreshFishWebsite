using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.StorageByIdGetters
{
    public class StorageGetterById : GetterById<Storage>
    {
        private readonly int? _id;
        public StorageGetterById(int? id, FreshFishDbContext context) : base(context) 
        {
            _id = id;
        }
        public override async Task<Storage> GetByIdAsync()
        {
            return await _context.Storages.FirstOrDefaultAsync(x => x.Id == _id);
        } 
    }
}
