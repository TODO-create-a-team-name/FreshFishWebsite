using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly IStorageRepository _storageRepo;
        private readonly FreshFishDbContext _context;
        public ProductsController(IProductRepository repo,
            IStorageRepository storageRepo,
            FreshFishDbContext context)
        {
            _repo = repo;
            _storageRepo = storageRepo;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public IActionResult Create(int storageId)
        {
            var model = new ProductViewModel
            {
                StorageId = storageId
            };
            return View(model);
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
                RemainingQuantityKg = model.Amount,
                ImageFile = model.ImageFile,
                Calories = model.Calories,
                Description = model.Description
            };
            if (ModelState.IsValid)
            {
                var storage = await _storageRepo.GetStorageByIdAsync(model.StorageId);
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

        public async Task<IActionResult> Edit(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var product = await _repo.GetProductByIdAsync(id);
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
