using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.OrderItemsByIdGetters
{
    public class OrderItemsWithProductsGetterById : GetterById<OrderItems>
    {
        private readonly int _storageId;
        private readonly int _id;
        public OrderItemsWithProductsGetterById(
            int storageId,
            int id,
            FreshFishDbContext context) : base(context)
        {
            _storageId = storageId;
            _id = id;
        }
        public override async Task<OrderItems> GetByIdAsync()
        {
            return await _context.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Include(o => o.Order)
            .ThenInclude(p => p.Products.Where(p => p.Product.StorageId == _storageId))
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(x => x.Id == _id && x.StorageId == _storageId);
        }
    }
}
