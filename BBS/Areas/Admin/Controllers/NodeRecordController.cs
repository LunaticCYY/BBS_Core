using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBS.Data;
using BBS.Models;

namespace BBS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NodeRecordController : Controller
    {
        private readonly BBSContext _context;

        public NodeRecordController(BBSContext context)
        {
            _context = context;
        }

        // GET: Admin/NodeRecord
        public async Task<IActionResult> Index()
        {
            var bBSContext = _context.NodeRecords.Include(n => n.Node).Include(n => n.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/NodeRecord/Details/5
        public async Task<IActionResult> Details(string id)
        {
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

        // GET: Admin/NodeRecord/Create
        public IActionResult Create()
        {
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/NodeRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NodeRecordId,UserId,NodeId,AddTime")] NodeRecord nodeRecord)
        {
            if (ModelState.IsValid)
            {
                nodeRecord.AddTime = DateTime.Now;
                _context.Add(nodeRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name", nodeRecord.NodeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", nodeRecord.UserId);
            return View(nodeRecord);
        }

        // GET: Admin/NodeRecord/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nodeRecord = await _context.NodeRecords.SingleOrDefaultAsync(m => m.NodeRecordId == id);
            if (nodeRecord == null)
            {
                return NotFound();
            }
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name", nodeRecord.NodeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", nodeRecord.UserId);
            return View(nodeRecord);
        }

        // POST: Admin/NodeRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NodeRecordId,UserId,NodeId,AddTime")] NodeRecord nodeRecord)
        {
            if (id != nodeRecord.NodeRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nodeRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NodeRecordExists(nodeRecord.NodeRecordId))
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
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name", nodeRecord.NodeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", nodeRecord.UserId);
            return View(nodeRecord);
        }

        // GET: Admin/NodeRecord/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
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

        // POST: Admin/NodeRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nodeRecord = await _context.NodeRecords.Include(a => a.User).Include(a => a.Node).SingleOrDefaultAsync(m => m.NodeRecordId == id);
            nodeRecord.Node = null;
            nodeRecord.User = null;
            _context.NodeRecords.Remove(nodeRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NodeRecordExists(string id)
        {
            return _context.NodeRecords.Any(e => e.NodeRecordId == id);
        }
    }
}
