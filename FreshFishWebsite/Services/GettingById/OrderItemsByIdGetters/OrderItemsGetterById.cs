using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.GettingById.OrderItemsByIdGetters
{
    public class OrderItemsGetterById : GetterById<OrderItems>
    {
        private readonly int _id;
        public OrderItemsGetterById(int id, FreshFishDbContext context) : base(context)
        {
            _id = id;
        }
        public override async Task<OrderItems> GetByIdAsync()
        {
            return await _context.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Include(o => o.Order)
            .ThenInclude(p => p.Products)
            .FirstOrDefaultAsync(x => x.Id == _id);
        }
    }
}
