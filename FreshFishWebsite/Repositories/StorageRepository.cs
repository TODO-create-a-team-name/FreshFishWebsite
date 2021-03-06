using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Services;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly FreshFishDbContext _;
        private readonly UserManager<User> _userManager;
        public StorageRepository(FreshFishDbContext context,
            UserManager<User> userManager)
        {
            _ = context;
            _userManager = userManager;
        }
        public IEnumerable<Storage> GetAll()
        => _.Storages.Include(p => p.Products).Include(d => d.Drivers);

        public IEnumerable<Storage> GetStoragesWithWorkers()
        => _.Storages.Include(d => d.Drivers).Include(a => a.StorageAdmin);

        public Storage GetById(int? id)
        => GetAll().FirstOrDefault(x => x.Id == id);

        public Storage GetByIdWithWorkers(int id)
        => GetStoragesWithWorkers().FirstOrDefault(x => x.Id == id);

        public async Task<OrderItems> GetByIdOrderItems(int id)
        => await _.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Include(o => o.Order)
            .ThenInclude(p => p.Products)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<OrderItems> GetByIdWithOrderAndProducts(int storageId, int id)
        => await _.OrderItems
            .Include(o => o.Order)
            .ThenInclude(u => u.User)
            .Include(o => o.Order)
            .ThenInclude(p => p.Products.Where(p => p.Product.StorageId == storageId))
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(x => x.Id == id && x.StorageId == storageId);

        public async Task<Storage> GetByIdWithOrderItems(int id)
        => await _.Storages
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Order)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Storage> GetByAdmin(string id)
        => await _.Storages
            .Include(p => p.Products)
            .Include(w => w.Drivers)
            .FirstOrDefaultAsync(x => x.StorageAdminId == id);

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

        public async Task<bool> AddStorageAdmin(User user, Storage storage, StorageViewModel model)
        {
            if (user != null && storage.StorageAdmin == null)
            {
                StorageAdmin storageAdmin = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Usersurname = user.Usersurname,
                    Company = user.Company,
                    CompanyAddress = user.CompanyAddress,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = true,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp,
                    ConcurrencyStamp = user.ConcurrencyStamp
                };
                await _userManager.DeleteAsync(user);
                await _userManager.CreateAsync(storageAdmin);
                await _userManager.AddToRoleAsync(storageAdmin, "AdminAssistant");
                storage.StorageAdmin = storageAdmin;
                await AddAsync(storage);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
