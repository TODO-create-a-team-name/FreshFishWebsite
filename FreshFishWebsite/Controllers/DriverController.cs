using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    [Authorize(Roles = "Driver")]
    public class DriverController : Controller
    {
        private readonly IDriverRepository _repo;
        private readonly UserManager<User> _userManager;
        public DriverController(IDriverRepository repo,
            UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
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
        public async Task<JsonResult> GetRequiredData()
        {
            var order = await _repo.GetOrderDetails(1);


            var user = await _userManager.GetUserAsync(User);
            var sAddr = user.CompanyAddress;
            var dAddr = order.Order.User.CompanyAddress;
            return new JsonResult(new
            {
                sAddr,
                dAddr
            });
        }

        //TODO: make a method that returns required data to build map route
    }
}
