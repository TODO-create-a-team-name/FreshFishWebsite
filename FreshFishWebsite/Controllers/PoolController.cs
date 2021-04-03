using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Services.GettingById.PoolByIdGetters;
using FreshFishWebsite.Services.GettingById.ProductByIdGetters;
using FreshFishWebsite.ViewModels;
using FreshFishWebsite.ViewModels.PoolVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    [Authorize(Roles = "AdminAssistant")]
    public class PoolController : Controller
    {
        private readonly IPoolRepository _repo;
        private readonly IProductRepository _productRepo;
        private readonly IProductInPoolRepository _productInPoolRepo;
        private readonly FreshFishDbContext _context;

        public PoolController(IPoolRepository repo,
            FreshFishDbContext context,
            IProductRepository productRepository,
            IProductInPoolRepository productInPoolRepo)
        {
            _repo = repo;
            _context = context;
            _productRepo = productRepository;
            _productInPoolRepo = productInPoolRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int storageId)
        {
            LitePoolViewModel model = new()
            {
                StorageId = storageId,
                Pools = await _repo.GetByIdAsync(new StoragePoolsGetterById(storageId, _context))
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManagePoolsIndex(int storageId)
        {
            LitePoolViewModel model = new()
            {
                StorageId = storageId,
                Pools = await _repo.GetByIdAsync(new StoragePoolsGetterById(storageId, _context))
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create(int storageId)
        {
            var model = new PoolViewModel
            {
                StorageId = storageId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pool model)
        {
            Pool pool = new()
            {
                PoolNumber = model.PoolNumber,
                MaxProductsKg = model.MaxProductsKg,
                RemainingSpaceForProducts = model.MaxProductsKg,
                StorageId = model.StorageId
            };
            await _repo.AddAsync(pool);
            return RedirectToAction("ManagePoolsIndex");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var pool = await _repo.GetByIdAsync(new PoolWithProductsGetterById(id, _context));
            var model = new DetailedPoolViewModel
            {
                Pool = pool,
                ProductsKgSum = _repo.GetProductsKgSum(pool)
            };
            return View(model);
        }

        [HttpPost] 
        public async Task<IActionResult> Edit()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AddProductsToPool(int storageId, int poolId)
        {
            var products = (await _productRepo.GetByIdAsync(new ProductGetterByStorageId(storageId, _context)))
                .Where(x => x.RemainingQuantityKg != 0);

            var pool = await _repo.GetByIdAsync(new PoolWithProductsGetterById(poolId, _context));
            var model = new ProductsForPoolViewModel
            {
                Products = products,
                StorageId = storageId,
                PoolId = poolId,
                Pool = pool
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductsToPool(ProductsForPoolViewModel model)
        {
            await _repo.AddProductsToPool(model);
            return RedirectToAction("AddProductsToPool", new { storageId = model.StorageId, poolId = model.PoolId});
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductInPool(int id, int storageId, int poolId)
        {
            await _productInPoolRepo.DeleteAsync(id);
            return RedirectToAction("AddProductsToPool", new { storageId, poolId});
        }

        public async Task<JsonResult> GetProductsForPoolData(int storageId, int poolId)
        {
            var products = (await _productRepo.GetByIdAsync(new ProductGetterByStorageId(storageId, _context)))
                .Select(x => new { x.Id, x.RemainingQuantityKg});

            int maxQuantity = await _repo.GetMaxAmountOfProductsInPool(poolId);

            return new JsonResult(new { products, maxQuantity });
        }
    }
}
