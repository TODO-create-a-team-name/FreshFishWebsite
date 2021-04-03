using FreshFishWebsite.Models;
using System.Threading.Tasks;

namespace FreshFishWebsite.AbstractClasses
{
    public abstract class GetterById<T>
    {
        protected readonly FreshFishDbContext _context;

        public GetterById(FreshFishDbContext context)
        { 
            _context = context;
        }

        public abstract Task<T> GetByIdAsync(); 
    }
}
