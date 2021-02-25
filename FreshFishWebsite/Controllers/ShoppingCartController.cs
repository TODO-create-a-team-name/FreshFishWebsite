﻿using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                await _repo.AddProductToShoppingCart(user, id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
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
