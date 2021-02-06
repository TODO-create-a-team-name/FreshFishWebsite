using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private IRepository<Product> _repo;
        public ProductsController(IRepository<Product> repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if(ModelState.IsValid)
            {
                await _repo.AddAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repo.DeleteAsync(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
