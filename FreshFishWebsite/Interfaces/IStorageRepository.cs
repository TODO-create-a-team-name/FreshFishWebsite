using FreshFishWebsite.Models;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IStorageRepository : IRepository<Storage>
    {
        public Task<Storage> GetByAdmin(string id);
    }
}
