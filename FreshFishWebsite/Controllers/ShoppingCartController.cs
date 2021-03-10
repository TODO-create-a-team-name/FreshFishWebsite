using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _repo;
        private readonly IRepository<Product> _productsRepo;
        private readonly FreshFishDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public ShoppingCartController(IShoppingCartRepository repo,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IRepository<Product> productsRepo,
            FreshFishDbContext context)
        {
            _repo = repo;
            _signInManager = signInManager;
            _userManager = userManager;
            _productsRepo = productsRepo;
            _context = context;
        }

        public IActionResult ShowAllProducts()
        {
            return View(_productsRepo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                var model = new ShoppingCartViewModel
                {
                    Products = _repo.GetShoppingCartItems(user)
                };
                model.TotalPrice = model.Products.Sum(p => p.Product.PricePerKg * p.Quantity);
                model.Count = model.Products.Count();

                return PartialView("_Shopping_Cart", model);
            }
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _repo.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShoppingCartProduct model)
        {
            await _repo.EditProductInShoppingCart(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteProductInShoppingCart(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                await _repo.AddProductToShoppingCart(user, id);
                return Ok();
            }
            return RedirectToAction("Login", "Account");
            //return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> OrderProducts()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _repo.OrderProducts(_userManager.GetUserId(User));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task IncrementOrDecrementQuantity(int id, int quantity)
        {
            await _repo.IncrementDecrementQuantity(id, quantity);
        }

        public JsonResult GetProductsData()
        {
            return new JsonResult(_context
            .Products
            .Select(p => new { p.Id, p.ProductName, p.PricePerKg, p.Date, p.Image, p.Description, p.Calories }));
        }
    }
}
