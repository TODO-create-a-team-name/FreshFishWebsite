using FreshFishWebsite.Models;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class AdminController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private readonly FreshFishDbContext _context;
        public AdminController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            FreshFishDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        { 
            if (_signInManager.IsSignedIn(User)) 
            {
                var user = await _userManager.GetUserAsync(User); 
                if (user != null) 
                {
                    var model = new ShowUserViewModel
                    {
                        Name = user.Name,
                        Surname = user.Usersurname, 
                        Email = user.Email
                    };

                    return View(model); 
                }
            }
            return View(); 
       }
        [Authorize(Roles = "MainAdmin")]
        public IActionResult GetOrders()
       {
            return View(_context.Orders
                .Include(u => u.User)
                .AsNoTracking());
       }

        [Authorize(Roles = "MainAdmin")]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var model = await _context.Orders
                .Include(u => u.User)
                .Include(p => p.Products)
                .ThenInclude(p => p.Product)
                .ThenInclude(s => s.Storage)
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(model);
        }

        [Authorize(Roles = "MainAdmin")]
        [HttpPost] 
        public async Task<IActionResult> SendOrdersToStorages(int id)
        {
            var order = await _context.Orders
                .Include(u => u.User)
                .Include(p => p.Products)
                .ThenInclude(p => p.Product)
                .ThenInclude(s => s.Storage)
                .FirstOrDefaultAsync(x => x.Id == id);

            var storages = order.Products.Select(x => x.Product.Storage).Distinct();
            
            foreach (var s in storages)
            {
                var orderItems = new List<OrderItems>
                {
                    new OrderItems
                    {
                        Order = order,
                        Storage = s
                    }
                };

                s.OrderItems.Add(orderItems.FirstOrDefault(x => x.Storage.Id == s.Id));
                await _context.OrderItems.AddAsync(orderItems.FirstOrDefault(x => x.Storage.Id == s.Id));
                _context.Storages.Update(s);
            }

            order.IsOrderAssigned = true;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetOrders");
        }
    }
}
