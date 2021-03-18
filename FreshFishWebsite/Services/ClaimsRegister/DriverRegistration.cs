using FreshFishWebsite.Interfaces.Registering;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services.ClaimsRegistration
{
    public class DriverRegistration : IRegisterClaim
    {
        private readonly FreshFishDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RegisterStorageWorker _model;
        public DriverRegistration(
            RegisterStorageWorker model,
            FreshFishDbContext context,
            UserManager<User> userManager)
        {
            _model = model;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityError>> Register()
        {
            var storage = _context.Storages.FirstOrDefault(s => s.Id == _model.StorageId);

            Driver driver = new()
            {
                Email = _model.Email,
                UserName = _model.Email,
                Name = _model.Name,
                EmailConfirmed = true,
                Usersurname = _model.Surname,
                Company = _model.Company,
                CompanyAddress = _model.CompanyAddress,
                Storage = storage
            };
            var result = await _userManager.CreateAsync(driver, _model.Password);
            await _userManager.AddToRoleAsync(driver, "Driver");
            storage.Drivers.Add(driver);
            _context.Storages.Update(storage);
            return result.Errors;
        }
    }
}
