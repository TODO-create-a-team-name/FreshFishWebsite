using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Repositories;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _repo;
        private readonly IRepository<Product> _productsRepo;
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

        public async Task<IActionResult> Edit(int id)
        {
            //var product = await _repo.GetById(id);
            //var model = new ShoppingCartProductViewModel
            //{
            //    ProductName = product.Product.ProductName,
            //    Quantity = product.Quantity
            //};

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

        [HttpPost]
        public async Task<IActionResult> OrderProducts()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _repo.OrderProducts(_userManager.GetUserId(User));
            }

            return RedirectToAction("Index");
        }
    }
}
