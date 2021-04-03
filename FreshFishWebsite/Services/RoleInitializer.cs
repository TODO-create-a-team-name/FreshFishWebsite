
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FreshFishWebsite.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "freshfishofficial@gmail.com";
            string password = "123password";
            if (await roleManager.FindByNameAsync("MainAdmin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("MainAdmin"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = "Fresh",
                    Usersurname = "Fish",
                    Company = "Freshfish",
                    CompanyAddress = "м Коломия, вул. Чехова 16",
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "MainAdmin");
                }
            }
        }
    }
}
