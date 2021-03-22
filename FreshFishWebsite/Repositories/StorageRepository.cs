using FreshFishWebsite.AbstractClasses;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Services.GettingById.StorageByIdGetters;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly FreshFishDbContext _context;
        private readonly UserManager<User> _userManager;
        public StorageRepository(FreshFishDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<T> GetByIdAsync<T>(GetterById<T> getter)
        {
           return await getter.GetByIdAsync();
        }

        public IEnumerable<Storage> GetAll()
        => _context.Storages.Include(p => p.Products).Include(d => d.Drivers).Include(a => a.StorageAdmin);

        public IEnumerable<Storage> GetStoragesWithWorkers()
        => _context.Storages.Include(d => d.Drivers);

       

        public async Task AddAsync(Storage newItem)
        {
            await _context.Storages.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Storage item)
        {
            _context.Storages.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var storage = await new StorageGetterById(id, _context).GetByIdAsync();
            if (storage != null)
            {
                _context.Storages.Remove(storage);
                await _context.SaveChangesAsync();
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
                StorageAdmin storageAdmin = await MakeUserAdminAndDeletePreviousAdminAsync(user);
                storage.StorageAdmin = storageAdmin;
                await AddAsync(storage);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method that updates a storage
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="model"></param>
        /// <returns>true, so that controller sends email with registration form for a brand new storage admin and a storage is updated</returns>
        /// <returns>false, so that controller update a storage (without updating a storage admin)</returns>
        /// <seealso cref="Controllers.StorageController.Edit(int?)"/>
        public async Task<bool> UpdateStorageAsync(Storage storage, StorageViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.StorageAdminEmail);

            storage.StorageNumber = model.StorageNumber;
            storage.Address = model.Address;

            if(user != null && user.Email != storage.StorageAdmin.Email)
            {
                StorageAdmin storageAdmin = await MakeUserAdminAndDeletePreviousAdminAsync(user);
                storage.StorageAdmin = storageAdmin;
                await UpdateAsync(storage);
                return true;
            }
            else
            {
                await UpdateAsync(storage);
                return false;
            }

        }

        private async Task<StorageAdmin> MakeUserAdminAndDeletePreviousAdminAsync(User user)
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
            return storageAdmin;

        }
    }
}
