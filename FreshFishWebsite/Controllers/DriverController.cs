using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    [Authorize(Roles = "Driver")]
    public class DriverController : Controller
    {
        private readonly IDriverRepository _repo;
        private readonly UserManager<User> _userManager;
        private readonly FreshFishDbContext _context;
        public DriverController(IDriverRepository repo,
            UserManager<User> userManager,
            FreshFishDbContext context)
        {
            _repo = repo;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var userId =  _userManager.GetUserId(User);
            return View(_repo.GetAssignedOrders(userId));
        }

        public IActionResult OrdersArchive()
        {
            var userId = _userManager.GetUserId(User);
            return View(_repo.GetDriverOrdersArchive(userId));
        }

        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var order = await _repo.GetOrderDetails(id);
            var model = new OrderDetailsForDriverViewModel
            {
                OrderItemsId = id,
                UserEmail = order.Order.User.Email,
                UserName = order.Order.User.Name,
                UserSurname = order.Order.User.Usersurname,
                CompanyName = order.Order.User.Company,
                Address = order.Order.User.CompanyAddress,
                Status = order.Status
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(OrderDetailsForDriverViewModel model)
        {
            await _repo.ChangeOrderStatus(model.OrderItemsId, model.Status);
            return RedirectToAction("Index");
        }
        public async Task<JsonResult> GetRequiredData(int id)
        {
            var userId =  _userManager.GetUserId(User);
            Driver driver = _context.Drivers
                .Include(s => s.Storage)
                .FirstOrDefault(x => x.Id == userId);

            var order = await _repo.GetOrderDetails(id);

            
            var storageAddress = driver.Storage.Address;
            var receiverAddress = order.Order.User.CompanyAddress;
            return new JsonResult(new
            {
                storageAddress,
                receiverAddress
            });
        }
    }
}
