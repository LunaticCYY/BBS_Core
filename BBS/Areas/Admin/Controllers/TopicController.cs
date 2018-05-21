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
    public class TopicController : Controller
    {
        private readonly BBSContext _context;
        public UserManager<User> UserManager { get; }

        public TopicController(BBSContext context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Admin/Topic
        public async Task<IActionResult> Index()
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var bBSContext = _context.Topics.Include(t => t.LastReplyUser).Include(t => t.Node).Include(t => t.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/Topic/Details/5
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

            var topic = await _context.Topics
                .Include(t => t.LastReplyUser)
                .Include(t => t.Node)
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Admin/Topic/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.LastReplyUser)
                .Include(t => t.Node)
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Admin/Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var topic = await _context.Topics.Include(t => t.LastReplyUser).Include(t => t.Node).Include(t => t.User).SingleOrDefaultAsync(m => m.TopicId == id);
            if(topic != null)
            {
                topic.User = null;
                topic.Node = null;
                topic.LastReplyUser = null;
            }
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(string id)
        {
            return _context.Topics.Any(e => e.TopicId == id);
        }
    }
}
