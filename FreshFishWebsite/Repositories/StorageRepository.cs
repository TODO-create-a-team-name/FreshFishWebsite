using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly FreshFishDbContext _;
        public StorageRepository(FreshFishDbContext context)
        {
            _ = context;
        }
        public IEnumerable<Storage> GetAll()
        => _.Storages.Include(p => p.Products).Include(d => d.Workers);

        public Storage GetById(int? id)
        => GetAll().FirstOrDefault(x => x.Id == id);

        public IEnumerable<Storage> GetStoragesWithWorkers()
        => _.Storages.Include(d => d.Workers);

        public Storage GetByIdWithWorkers(int id)
        => GetStoragesWithWorkers().FirstOrDefault(x => x.Id == id);

        public async Task<Storage> GetByAdmin(string id)
        => await _.Storages.Include(p => p.Products).Include(w => w.Workers).FirstOrDefaultAsync(x => x.AdminId == id);
        
        public async Task AddAsync(Storage newItem)
        {
            await _.Storages.AddAsync(newItem);
            await _.SaveChangesAsync();
        }
        public async Task UpdateAsync(Storage item)
        {
            _.Storages.Update(item);
            await _.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var storage = GetById(id);
            if (storage != null)
            {
                _.Storages.Remove(storage);
                await _.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
