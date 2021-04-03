using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.Repositories;
using FreshFishWebsite.Services;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class StorageController : Controller
    {
        private readonly IStorageRepository _repo;
        private readonly FreshFishDbContext _context;
        private readonly UserManager<User> _userManager;
        public StorageController(IStorageRepository repo,
            UserManager<User> userManager,
            FreshFishDbContext context)
        {
            _repo = repo;
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "MainAdmin")]
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }
        [Authorize(Roles = "MainAdmin")]
        public IActionResult Create()
        {
            return PartialView("_Create_Storage");
        }
        [Authorize(Roles = "MainAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(StorageViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.StorageAdminEmail);
      
            if (ModelState.IsValid)
            {
                var storage = new Storage
                {
                    StorageNumber = model.StorageNumber,
                    Address = model.Address
                };
                if (await new StorageRepository(_context, _userManager).AddStorageAdminAsync(user, storage, model))
                {
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


        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> CheckEmail(string StorageAdminEmail)
        {
            var user = await _userManager.FindByEmailAsync(StorageAdminEmail);
            if (user != null)
            {
                if(user.GetType() == typeof(StorageAdmin)
                    || user.GetType() == typeof(Driver))
                return Json(false); 
                else return Json(true);
            }
            return Json(true);
            
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

            var storage = await _repo.GetStorageByIdAsync(model.StorageId);
            if (storage == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(ModelState.IsValid)
            {
                if(user != null)
                {
                    Driver storageDriver = new()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Usersurname = user.Usersurname,
                        Company = user.Company,
                        CompanyAddress = user.CompanyAddress,
                        UserName = user.UserName, 
                        Email = user.Email,
                        EmailConfirmed = true,
                        PasswordHash = user.PasswordHash,
                        SecurityStamp = user.SecurityStamp,
                        ConcurrencyStamp = user.ConcurrencyStamp,
                        Storage = storage
                    };
                    await _userManager.DeleteAsync(user);
                    await _userManager.CreateAsync(storageDriver);
                    await _userManager.AddToRoleAsync(storageDriver, "Driver");
                    storage.Drivers.Add(storageDriver);
                    await _repo.UpdateAsync(storage);
                    await new EmailService().SendEmailAsync(storageDriver.Email, "Водій складу FreshFish",
                       $"Ви тепер водій складу №{storage.StorageNumber}");
                    return RedirectToAction("GetStorage");
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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var storage = await _repo.GetStorageByIdAsync(id);
            var model = new StorageViewModel
            {
                StorageNumber = storage.StorageNumber,
                Address = storage.Address,
                StorageAdminEmail = storage.StorageAdmin.Email
            };
            if (storage == null)
            {
                return NotFound();
            }

            return PartialView("_Edit_Storage",model);
        }
        [Authorize(Roles = "MainAdmin")]
        [HttpPost]
        public async Task<IActionResult> Edit(StorageViewModel model)
        {
            var storage = await _repo.GetStorageByIdAsync(model.Id);
            if (await _repo.UpdateStorageAsync(storage, model))
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (storage.StorageAdmin.Email == model.StorageAdminEmail)
                {
                    return RedirectToAction("Index");
                }
                else
                {
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
        public async Task<IActionResult> Details(int id)
        {
            return View(await _repo.GetStorageByIdAsync(id));
        }

        [HttpGet]
        [Authorize(Roles = "AdminAssistant")]
        public async Task<IActionResult> GetStorage()
        {
            var storageAdmin = await _userManager.GetUserAsync(User);

            return View(await _repo.GetStorageByAdminIdAsync(storageAdmin.Id));
        }

        [HttpGet]
        [Authorize(Roles = "AdminAssistant")]
        public async Task<IActionResult> GetStorageOrders(int storageId)
        {
            return View(await _repo.GetStorageWithOrderItemsAsync(storageId));
        }

        [HttpGet]
        [Authorize(Roles = "AdminAssistant")]
        public async Task<IActionResult> GetOrderDetails(int storageId, int orderItemsId)
        {
            var info = await _repo.GetOrderItemsWithProductsByIdAsync(orderItemsId, storageId);
            var model = new OrderDetailsViewModel
            {
                OrderItemsId = orderItemsId,
                UserEmail = info.Order.User.Email,
                UserName = info.Order.User.Name,
                UserSurname = info.Order.User.Usersurname,
                CompanyName = info.Order.User.Company,
                Address = info.Order.User.CompanyAddress,
                Products = info.Order.Products,
                Drivers = _context.Drivers
                          .Where(x => !x.IsDelivering)
                          .Select(x => new SelectListItem()
                          {
                              Value = x.Id,
                              Text = $"{x.Email} ({x.Name} {x.Usersurname})"
                          }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "AdminAssistant")]
        public async Task<IActionResult> AssignOrderToDriver(OrderDetailsViewModel model)
        {
            var driver = await _context
                .Drivers
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == model.DriverId);
            var order = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == model.OrderItemsId);
            driver.OrderItems.Add(order);
            driver.IsDelivering = true;
            order.IsAssigned = true;
            _context.OrderItems.Update(order);
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
