using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    [Authorize(Roles = "MainAdmin, AdminAssistant")]
    public class AdminController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        public AdminController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        { 
            if (_signInManager.IsSignedIn(User)) 
            {
                var user = await _userManager.GetUserAsync(User); 
                if (user != null) 
                {
                    var model = new ShowUserViewModel
                    {
                        Name = user.Name,
                        Surname = user.Usersurname, 
                        Email = user.Email
                    };

                    return View(model); 
                }
            }
            return View(); 
       }
    }
}
