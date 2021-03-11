using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IDriverRepository
    {
        public IEnumerable<OrderItems> GetAssignedOrders(string driverId);
        public IEnumerable<OrderItems> GetDriverOrdersArchive(string driverId);
        public Task<JsonResult> GetOrderDetailsJson(int orderId, string userId);
        public Task<OrderItems> GetOrderDetails(int id);
        public Task ChangeOrderStatus(int orderId, OrderStatus status);
    }
}
