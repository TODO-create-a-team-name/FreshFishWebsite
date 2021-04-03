using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.ProductByIdGetters
{
    public class ProductGetterByStorageId : GetterById<IEnumerable<Product>>
    {
        private readonly int _storageId;
        public ProductGetterByStorageId(int id, FreshFishDbContext context) : base(context)
        {
            _storageId = id;
        }
        public override async Task<IEnumerable<Product>> GetByIdAsync()
        {
            return await Task.Run(() => _context.Products.Where(s => s.StorageId == _storageId));
        }
    }
}
