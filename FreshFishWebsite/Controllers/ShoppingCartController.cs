using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _repo;
        private IRepository<Product> _productsRepo;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public ShoppingCartController(IShoppingCartRepository repo,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IRepository<Product> productsRepo)
        {
            _repo = repo;
            _signInManager = signInManager;
            _userManager = userManager;
            _productsRepo = productsRepo;
        }

        public IActionResult ShowAllProducts()
        {
            return View(_productsRepo.GetAll());
        }

        public async Task<IActionResult> Index()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                return View(_repo.GetShoppingCartItems(user));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                await _repo.AddProductToShoppingCart(user, id);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
