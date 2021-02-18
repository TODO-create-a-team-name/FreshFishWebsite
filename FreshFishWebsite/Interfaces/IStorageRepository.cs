using FreshFishWebsite.Models;
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
    }
}
