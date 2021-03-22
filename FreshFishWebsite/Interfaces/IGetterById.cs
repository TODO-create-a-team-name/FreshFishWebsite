using FreshFishWebsite.AbstractClasses;
using System.Threading.Tasks;

namespace FreshFishWebsite.Interfaces
{
    public interface IGetterById
    {
        public Task<T> GetByIdAsync<T>(GetterById<T> getter);
    }
}
