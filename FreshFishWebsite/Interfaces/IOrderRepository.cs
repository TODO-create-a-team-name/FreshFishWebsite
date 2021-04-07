using FreshFishWebsite.Models;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IOrderRepository : ICrud<Order>
    {
        public Task<Order> GetOrderWithUserAndProductsWithStorages(int orderId);

        public Task SendOrderToStorages(int orderId);
    }
}
