using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private IRepository<Product> _repo;
        private IStorageRepository _storageRepo;
        public ProductsController(IRepository<Product> repo,
            IStorageRepository storageRepo)
        {
            _repo = repo;
            _storageRepo = storageRepo;
        }

        public IActionResult Create(int storageId)
        {
            var model = new ProductViewModel
            {
                StorageId = storageId
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                PricePerKg = model.Price,
                Date = model.Date,
                QuantityKg = model.Amount,
                ImageFile = model.ImageFile
            };
            if (ModelState.IsValid)
            {
                var storage = _storageRepo.GetById(model.StorageId);
                product.Storage = storage;
                await _repo.AddAsync(product);
                return RedirectToAction("GetStorage", "Storage");
            }
            return View(product);
        }

        //public async Task<IActionResult> Create(ProductViewModel model)
        //{
        //    var product = new Product
        //    {
        //        ProductName = model.ProductName,
        //        PricePerKg = model.Price,
        //        Date = model.Date,
        //        QuantityKg = model.Amount,
        //        ImageFile = model.ImageFile
        //    };
        //    if (ModelState.IsValid)
        //    {
        //        var storage = _storageRepo.GetById(model.StorageId);
        //        product.Storage = storage;
        //        await _repo.AddAsync(product);
        //        return RedirectToAction("GetStorage", "Storage");
        //    }
        //    return View(product);
        //}

        public IActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }

            var product = _repo.GetById(id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            await _repo.UpdateAsync(product);
            return RedirectToAction("GetStorage", "Storage");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repo.DeleteAsync(id))
                return RedirectToAction("GetStorage", "Storage");
            else
                return NotFound();
        }
    }
}
