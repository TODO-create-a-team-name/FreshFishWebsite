using FreshFishWebsite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class PoolController : Controller
    {
        private readonly IPoolRepository _repo;
        public PoolController(IPoolRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index(int id)
        {
            var model = _repo.GetStoragePools(id);
            return View(model);
        }

        public IActionResult ManagePoolsIndex(int id)
        {
            var model = _repo.GetStoragePools(id);
            return View(model);
        }


    }
}
