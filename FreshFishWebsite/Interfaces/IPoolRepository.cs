using FreshFishWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IPoolRepository : IRepository<Pool>
    {
        public IEnumerable<Pool> GetStoragePools(int id);
    }
}
