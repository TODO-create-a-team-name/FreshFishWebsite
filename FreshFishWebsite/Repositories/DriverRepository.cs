using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly FreshFishDbContext _context;
        public DriverRepository(FreshFishDbContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderItems> GetAssignedOrders(string driverId)
        => _context.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Where(o => o.DriverId == driverId && !o.IsDelivered)
            .AsNoTracking();
        public IEnumerable<OrderItems> GetDriverOrdersArchive(string driverId)
        => _context.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Where(o => o.DriverId == driverId && o.IsDelivered)
            .AsNoTracking();

        public async Task<JsonResult> GetOrderDetailsJson(int orderId, string userId)
        {
            var order = await _context.OrderItems
              .Include(o => o.Order)
              .ThenInclude(u => u.User)
              .FirstOrDefaultAsync(x => x.Id == orderId);
           
            Driver driver = _context.Drivers
               .Include(s => s.Storage)
               .FirstOrDefault(x => x.Id == userId);


            var storageAddress = driver.Storage.Address;
            var receiverAddress = order.Order.User.CompanyAddress;
           
            return new JsonResult(new
            {
                storageAddress,
                receiverAddress
            });
        }

        public async Task ChangeOrderStatus(int orderId, OrderStatus status, string driverId)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == driverId);
            var order = await GetOrderDetails(orderId);
            order.Status = status;
            if(status == OrderStatus.Delivered)
            {
                driver.IsDelivering = false;
                _context.Drivers.Update(driver);
                order.IsDelivered = true;
            }
            _context.OrderItems.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderItems> GetOrderDetails(int orderId)
        {
            return await _context.OrderItems
              .Include(o => o.Order)
              .ThenInclude(u => u.User)
              .FirstOrDefaultAsync(x => x.Id == orderId);
        }
    }
}
