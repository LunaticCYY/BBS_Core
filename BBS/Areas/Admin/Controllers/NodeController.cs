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
    public class NodeController : Controller
    {
        private readonly BBSContext _context;
        public UserManager<User> UserManager { get; }

        public NodeController(BBSContext context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Admin/Node
        public async Task<IActionResult> Index()
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var bBSContext = _context.Nodes.Include(n => n.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/Node/Details/5
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

            var node = await _context.Nodes
                .Include(n => n.User)
                .SingleOrDefaultAsync(m => m.NodeId == id);
            if (node == null)
            {
                return NotFound();
            }

            return View(node);
        }

        // GET: Admin/Node/Create
        public IActionResult Create()
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ViewBag.UserId = new SelectList(_context.Users, "Id", "UserName");
            ViewBag.ParentId = new SelectList(_context.Nodes.Where(a => a.IsParent == IsParent.Parent), "NodeId", "Name");
            return View();
        }

        // POST: Admin/Node/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NodeId,Name,UserId,IsParent,ParentId,AddTime")] Node node)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            if (ModelState.IsValid)
            {
                node.AddTime = DateTime.Now;
                if(node.IsParent == IsParent.Parent)
                {
                    node.ParentId = string.Empty;
                }
                _context.Add(node);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserId = new SelectList(_context.Users, "Id", "UserName", node.UserId);
            ViewBag.ParentId = new SelectList(_context.Nodes.Where(a => a.IsParent == IsParent.Parent), "NodeId", "Name", node.ParentId);
            return View(node);
        }

        // GET: Admin/Node/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var node = await _context.Nodes.SingleOrDefaultAsync(m => m.NodeId == id);
            if (node == null)
            {
                return NotFound();
            }
            ViewBag.UserId = new SelectList(_context.Users, "Id", "UserName");
            ViewBag.ParentId = new SelectList(_context.Nodes.Where(a => a.IsParent == IsParent.Parent), "NodeId", "Name");
            return View(node);
        }

        // POST: Admin/Node/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NodeId,Name,UserId,IsParent,ParentId,AddTime")] Node node)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            if (id != node.NodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(node);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NodeExists(node.NodeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserId = new SelectList(_context.Users, "Id", "UserName", node.UserId);
            ViewBag.ParentId = new SelectList(_context.Nodes.Where(a => a.IsParent == IsParent.Parent), "NodeId", "Name", node.ParentId);
            return View(node);
        }

        // GET: Admin/Node/Delete/5
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

            var node = await _context.Nodes
                .Include(n => n.User)
                .SingleOrDefaultAsync(m => m.NodeId == id);
            if (node == null)
            {
                return NotFound();
            }

            return View(node);
        }

        // POST: Admin/Node/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!UserManager.GetUserName(User).ToLower().Equals("admin"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var node = await _context.Nodes.Include(n => n.User).SingleOrDefaultAsync(m => m.NodeId == id);
            node.User = null;
            _context.Nodes.Remove(node);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NodeExists(string id)
        {
            return _context.Nodes.Any(e => e.NodeId == id);
        }
    }
}
