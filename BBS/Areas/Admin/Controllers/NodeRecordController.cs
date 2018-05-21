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
    public class NodeRecordController : Controller
    {
        private readonly BBSContext _context;
        public UserManager<User> UserManager { get; }

        public NodeRecordController(BBSContext context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Admin/NodeRecord
        public async Task<IActionResult> Index()
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var bBSContext = _context.NodeRecords.Include(n => n.Node).Include(n => n.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/NodeRecord/Details/5
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

            var nodeRecord = await _context.NodeRecords
                .Include(n => n.Node)
                .Include(n => n.User)
                .SingleOrDefaultAsync(m => m.NodeRecordId == id);
            if (nodeRecord == null)
            {
                return NotFound();
            }

            return View(nodeRecord);
        }

        private bool NodeRecordExists(string id)
        {
            return _context.NodeRecords.Any(e => e.NodeRecordId == id);
        }
    }
}
