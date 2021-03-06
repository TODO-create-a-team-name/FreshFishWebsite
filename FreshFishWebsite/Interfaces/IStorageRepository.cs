using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IStorageRepository : IRepository<Storage>
    {
        public Task<Storage> GetByAdmin(string id);

        public IEnumerable<Storage> GetStoragesWithWorkers();

        public Storage GetByIdWithWorkers(int id);

        public Task<Storage> GetByIdWithOrderItems(int id);

        public Task<OrderItems> GetByIdWithOrderAndProducts(int storageId, int id);

        public Task<OrderItems> GetByIdOrderItems(int id);

        public Task<bool> AddStorageAdmin(User user, Storage storage, StorageViewModel model);
    }
}
