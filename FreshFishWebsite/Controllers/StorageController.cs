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
        public StorageController(IStorageRepository repo,
            UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
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
                    StorageNumber = model.StorageNumber,
                    Address = model.Address
                };
                if(storageAdmin != null)
                {
                    await _userManager.AddToRoleAsync(storageAdmin, "AdminAssistant");
                    storage.Workers.Add(storageAdmin);
                    storage.AdminId = storageAdmin.Id;
                    await _repo.AddAsync(storage);
                    await new EmailService().SendEmailAsync(model.StorageAdminEmail, "Адміністратор складу FreshFish",
                       $"Ви тепер адміністратор складу №{storage.StorageNumber}");
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
                       $"Тепер ви адміністратор складу №{storage.StorageNumber}: <a href='{callbackUrl}'>Підтвердити</a>");
                    return Content("Адміністратор складу, якого ви призначили, скоро отримає повідомлення про реєстрацію на електронній пошті.");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "AdminAssistant")]
        [HttpGet]
        public IActionResult AddDriver(int storageId)
        {
            var model = new CreateStorageWorkerViewModel
            {
                StorageId = storageId
            };
            return View(model);
        }

        [Authorize(Roles = "AdminAssistant")]
        [HttpPost]
        public async Task<IActionResult> AddDriver(CreateStorageWorkerViewModel model)
        {
            var storage = _repo.GetByIdWithWorkers(model.StorageId);
            if (storage == null)
            {
                return NotFound();
            }
            var storageDriver = await _userManager.FindByEmailAsync(model.Email);
            if(ModelState.IsValid)
            {
                if(storageDriver != null)
                {
                    await _userManager.AddToRoleAsync(storageDriver, "Driver");
                    storage.Workers.Add(storageDriver);
                    await _repo.UpdateAsync(storage);
                    await new EmailService().SendEmailAsync(storageDriver.Email, "Водій складу FreshFish",
                       $"Ви тепер водій складу №{storage.StorageNumber}");
                    return RedirectToAction("Index");
                }
                else
                {
                    var callbackUrl = Url.Action(
                       "RegisterStorageDriver",
                       "Account",
                       new { storageId = storage.Id, email = model.Email },
                       protocol: HttpContext.Request.Scheme);

                    await new EmailService().SendEmailAsync(model.Email, "Водій складу FreshFish",
                       $"Тепер ви водій складу №{storage.StorageNumber}: <a href='{callbackUrl}'>Підтвердити</a>");
                    return Content("Водій складу, якого ви призначили, скоро отримає повідомлення про реєстрацію на електронній пошті.");
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
