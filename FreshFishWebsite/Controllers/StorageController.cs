using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Services;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    [Authorize(Roles = "MainAdmin")]
    public class StorageController : Controller
    {
        private readonly IRepository<Storage> _repo;
        private readonly UserManager<User> _userManager;
        public StorageController(IRepository<Storage> repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
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
        public async Task<IActionResult> Create(CreateStorageViewModel model)
        {
            var storageAdmin = await _userManager.FindByEmailAsync(model.StorageAdminEmail);
            if (ModelState.IsValid)
            {
                var storage = new Storage
                {
                    Address = model.Address
                };
                if(storageAdmin != null)
                {
                    await _userManager.AddToRoleAsync(storageAdmin, "AdminAssistant");
                    storage.Workers.Add(storageAdmin);
                    await _repo.AddAsync(storage);
                    return RedirectToAction("Index");
                }
                else
                {
                    await _repo.AddAsync(storage);
                    var callbackUrl = Url.Action(
                        "RegisterStorageAdmin",
                        "Account",
                        new { storageId = storage.Id, adminEmail = model.StorageAdminEmail },
                        protocol: HttpContext.Request.Scheme);

                    await new EmailService().SendEmailAsync(model.StorageAdminEmail, "Адміністратор складу FreshFish",
                       $"Тепер ви адміністратор складу №{storage.Id}: <a href='{callbackUrl}'>Підтвердити</a>");
                    return Content("Адміністратор складу, якого ви призначили, скоро отримає повідомлення про реєстрацію на електронній пошті.");
                }
            }
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var storage = _repo.GetById(id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Storage storage)
        {
            await _repo.UpdateAsync(storage);
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_repo.GetById(id));
        }

    }
}
