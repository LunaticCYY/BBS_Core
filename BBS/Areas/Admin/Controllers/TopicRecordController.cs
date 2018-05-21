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
    public class TopicRecordController : Controller
    {
        private readonly BBSContext _context;
        public UserManager<User> UserManager { get; }

        public TopicRecordController(BBSContext context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Admin/TopicRecord
        public async Task<IActionResult> Index()
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var bBSContext = _context.TopicRecords.Include(t => t.Topic).Include(t => t.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/TopicRecord/Details/5
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

            var topicRecord = await _context.TopicRecords
                .Include(t => t.Topic)
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.TopicRecordId == id);
            if (topicRecord == null)
            {
                return NotFound();
            }

            return View(topicRecord);
        }

        private bool TopicRecordExists(string id)
        {
            return _context.TopicRecords.Any(e => e.TopicRecordId == id);
        }
    }
}
