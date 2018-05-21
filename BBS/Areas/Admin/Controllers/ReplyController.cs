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
    public class ReplyController : Controller
    {
        private readonly BBSContext _context;
        public UserManager<User> UserManager { get; }

        public ReplyController(BBSContext context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Admin/Reply
        public async Task<IActionResult> Index()
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var bBSContext = _context.Replys.Include(r => r.Topic).Include(r => r.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/Reply/Details/5
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

            var reply = await _context.Replys
                .Include(r => r.Topic)
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // GET: Admin/Reply/Delete/5
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

            var reply = await _context.Replys
                .Include(r => r.Topic)
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // POST: Admin/Reply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var reply = await _context.Replys.Include(r => r.Topic).Include(r => r.User).SingleOrDefaultAsync(m => m.ReplyId == id);
            reply.Topic = null;
            reply.User = null;
            _context.Replys.Remove(reply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplyExists(string id)
        {
            return _context.Replys.Any(e => e.ReplyId == id);
        }
    }
}
