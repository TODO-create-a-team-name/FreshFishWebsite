using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IStorageRepository : ICrud<Storage>
    {
        public IEnumerable<Storage> GetStoragesWithDrivers();
        public Task<Storage> GetStorageByIdAsync(int storageId);
        public Task<Storage> GetStorageByAdminIdAsync(string adminId);
        public Task<Storage> GetStorageWithOrderItemsAsync(int storageId);
        public Task<Storage> GetStorageWithDriversAsync(int storageId);
        public Task<OrderItems> GetOrderItemsByIdAsync(int orderItemsId);
        public Task<OrderItems> GetOrderItemsWithProductsByIdAsync(int orderItemsId, int storageId);
        public Task<bool> AddStorageAdminAsync(User user, Storage storage, StorageViewModel model);
        public Task<bool> UpdateStorageAsync(Storage storage, StorageViewModel model);
    }
}
