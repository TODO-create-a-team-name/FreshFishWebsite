using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.ProductByIdGetters
{
    public class ProductGetterById : GetterById<Product>
    {
        private readonly int? _id;
        public ProductGetterById(int? id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }
        public override async Task<Product> GetByIdAsync()
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == _id);
        }
    }
}
