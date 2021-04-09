using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IOrderRepository _repo;
        public AdminController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IOrderRepository repo)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _repo = repo;
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
        [Authorize(Roles = "MainAdmin")]
        public IActionResult GetOrders()
       {
            return View(_repo.GetAll());
       }

        [Authorize(Roles = "MainAdmin")]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var model = await _repo.GetOrderWithUserAndProductsWithStorages(id);

            return View(model);
        }

        [Authorize(Roles = "MainAdmin")]
        [HttpPost] 
        public async Task<IActionResult> SendOrdersToStorages(int id)
        {
            await _repo.SendOrderToStorages(id);
            return RedirectToAction("GetOrders");
        }
    }
}
