using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly FreshFishDbContext _;
        public DriverRepository(FreshFishDbContext context)
        {
            _ = context;
        }
        public IEnumerable<OrderItems> GetAssignedOrders(string driverId)
        => _.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Where(o => o.DriverId == driverId && !o.IsDelivered)
            .AsNoTracking();
        public IEnumerable<OrderItems> GetDriverOrdersArchive(string driverId)
        => _.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Where(o => o.DriverId == driverId && o.IsDelivered)
            .AsNoTracking();

        public async Task<OrderItems> GetOrderDetails(int orderId)
        => await _.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(x => x.Id == orderId);

        public async Task ChangeOrderStatus(int orderId, OrderStatus status)
        {
            var order = await GetOrderDetails(orderId);
            order.Status = status;
            _.OrderItems.Update(order);
            await _.SaveChangesAsync();
        }

    }
}
