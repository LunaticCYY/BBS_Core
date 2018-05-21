using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBS.Data;
using BBS.Models;
using Microsoft.AspNetCore.Identity;
using BBS.Controllers;

namespace BBS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FollowRecordController : Controller
    {
        private readonly BBSContext _context;
        public UserManager<User> UserManager { get; }

        public FollowRecordController(BBSContext context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Admin/FollowRecord
        public async Task<IActionResult> Index()
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var bBSContext = _context.FollowRecords.Include(f => f.FollowUser).Include(f => f.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/FollowRecord/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var followRecord = await _context.FollowRecords
                .Include(f => f.FollowUser)
                .Include(f => f.User)
                .SingleOrDefaultAsync(m => m.FollowRecordId == id);
            if (followRecord == null)
            {
                return NotFound();
            }

            return View(followRecord);
        }

        private bool FollowRecordExists(string id)
        {
            return _context.FollowRecords.Any(e => e.FollowRecordId == id);
        }
    }
}
