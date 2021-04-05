using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class StorageDbContextExtentions
    {
        public static IEnumerable<Storage> GetStorages(this DbSet<Storage> storages)
        {
            return storages
                         .Include(p => p.Products)
                         .Include(d => d.Drivers)
                         .Include(a => a.StorageAdmin);

        }
        public static IEnumerable<Storage> GetStoragesWithDrivers(this DbSet<Storage> storages)
        {
            return storages.Include(d => d.Drivers);
        }
        public static async Task<Storage> GetStorageByIdAsync(this DbSet<Storage> storages, int id)
        {
            return await storages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<Storage> GetStorageByAdminIdAsync(this DbSet<Storage> storages, string id)
        {
            return await storages
            .Include(p => p.Products)
            .Include(w => w.Drivers)
            .FirstOrDefaultAsync(x => x.StorageAdminId == id);
        }

        public static async Task<Storage> GetStorageWithOrderItemsByIdAsync(this DbSet<Storage> storages, int id)
        {
            return await storages
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Order)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<Storage> GetStorageWithDriversByIdAsync(this DbSet<Storage> storages, int id)
        {
            return await storages   
                .Include(d => d.Drivers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
