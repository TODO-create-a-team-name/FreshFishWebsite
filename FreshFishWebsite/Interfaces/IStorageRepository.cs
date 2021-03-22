using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IStorageRepository : ICrud<Storage>, IGetterById
    {
        public Task<bool> AddStorageAdmin(User user, Storage storage, StorageViewModel model);

        public Task<bool> UpdateStorageAsync(Storage storage, StorageViewModel model);
    }
}
