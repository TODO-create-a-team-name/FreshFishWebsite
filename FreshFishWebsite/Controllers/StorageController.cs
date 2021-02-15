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
    public class StorageController : Controller
    {
        private readonly IStorageRepository _repo;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public StorageController(IStorageRepository repo,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _repo = repo;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "MainAdmin")]
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }
        [Authorize(Roles = "MainAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "MainAdmin")]
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
                    storage.AdminId = storageAdmin.Id;
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
        [Authorize(Roles = "MainAdmin")]
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
        [Authorize(Roles = "MainAdmin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Storage storage)
        {
            await _repo.UpdateAsync(storage);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "MainAdmin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repo.DeleteAsync(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
        [Authorize(Roles = "MainAdmin")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_repo.GetById(id));
        }

        [HttpGet]
        [Authorize(Roles = "AdminAssistant")]
        public async Task<IActionResult> GetStorage()
        {
            var user = await _userManager.GetUserAsync(User);
            //if (await _userManager.IsInRoleAsync(user, "AdminAssistant"))
            //{

            //}

            return View(await _repo.GetByAdmin(user.Id));
        }

    }
}
