using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface ICrud<T>
    {
        IEnumerable<T> GetAll();
        Task AddAsync(T newItem);
        Task UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
    }
}
