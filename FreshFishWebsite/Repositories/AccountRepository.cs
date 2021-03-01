

using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Repositories
{
    public class AccountRepository<T> : IAccountRepository<T> where T : class
    {
        private readonly UserManager<User> _userManager;
        private readonly FreshFishDbContext _context;
        public AccountRepository(UserManager<User> userManager, FreshFishDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(RegisterStorageWorker model)
        {
            var storage = _context.Storages.FirstOrDefault(s => s.Id == model.StorageId);
            IdentityResult result = new();
            if(typeof(T) == typeof(StorageAdmin))
            {
                StorageAdmin user = new()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    EmailConfirmed = true,
                    Usersurname = model.Surname,
                    Company = model.Company,
                    CompanyAddress = model.CompanyAddress,
                    Storage = storage
                };
                result = await _userManager.CreateAsync((StorageAdmin)(object)user, model.Password);
                await _userManager.AddToRoleAsync((StorageAdmin)(object)user, "AdminAssistant");
                storage.StorageAdmin = (StorageAdmin)(object)user;
                _context.Storages.Update(storage);
            }
            else if(typeof(T) == typeof(Driver))
            {
                Driver user = new()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    EmailConfirmed = true,
                    Usersurname = model.Surname,
                    Company = model.Company,
                    CompanyAddress = model.CompanyAddress,
                    Storage = storage
                };
                result = await _userManager.CreateAsync((Driver)(object)user, model.Password);
                await _userManager.AddToRoleAsync((Driver)(object)user, "Driver");
                storage.Drivers.Add((Driver)(object)user);
                _context.Storages.Update(storage);
            }
            await _context.SaveChangesAsync();
            return result.Errors;
        }
    }
}
