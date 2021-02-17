using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    [Authorize(Roles = "MainAdmin, AdminAssistant")]
    public class AdminController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private readonly FreshFishDbContext _context;
        public AdminController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            FreshFishDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
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

       public IActionResult GetOrders()
       {
            return View(_context.Orders
                .Include(u => u.User)
                .AsNoTracking());
       }
    }
}
