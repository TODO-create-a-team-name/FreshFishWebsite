using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.PoolByIdGetters
{
    public class StoragePoolsGetterById : GetterById<IEnumerable<Pool>>
    {
        private readonly int _storageId;
        public StoragePoolsGetterById(int id, FreshFishDbContext context) : base(context)
        {
            _storageId = id;
        }
        public override async Task<IEnumerable<Pool>> GetByIdAsync()
        {
            return await Task.Run(() => _context.Pools.Where(p => p.StorageId == _storageId));
        }
    }
}
