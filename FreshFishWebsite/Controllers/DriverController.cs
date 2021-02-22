using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
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
                Address = order.Order.User.CompanyAddress
                
            };
            return View(model);
        }

        //TODO: make a method that returns required data to build map route
    }
}
