using FreshFishWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IDriverRepository
    {
        public IEnumerable<OrderItems> GetAssignedOrders(string driverId);
        public IEnumerable<OrderItems> GetDriverOrdersArchive(string driverId);
        public Task<OrderItems> GetOrderDetails(int orderId);
        public Task ChangeOrderStatus(int orderId, OrderStatus status);
    }
}
