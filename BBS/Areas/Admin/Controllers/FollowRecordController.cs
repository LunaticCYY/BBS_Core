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
    public class FollowRecordController : Controller
    {
        private readonly BBSContext _context;

        public FollowRecordController(BBSContext context)
        {
            _context = context;
        }

        // GET: Admin/FollowRecord
        public async Task<IActionResult> Index()
        {
            var bBSContext = _context.FollowRecords.Include(f => f.FollowUser).Include(f => f.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/FollowRecord/Details/5
        public async Task<IActionResult> Details(string id)
        {
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

        // GET: Admin/FollowRecord/Create
        public IActionResult Create()
        {
            ViewData["FollowUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/FollowRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FollowRecordId,UserId,FollowUserId,AddTime")] FollowRecord followRecord)
        {
            if (ModelState.IsValid)
            {
                followRecord.AddTime = DateTime.Now;
                _context.Add(followRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FollowUserId"] = new SelectList(_context.Users, "Id", "UserName", followRecord.FollowUserId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", followRecord.UserId);
            return View(followRecord);
        }

        // GET: Admin/FollowRecord/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followRecord = await _context.FollowRecords.SingleOrDefaultAsync(m => m.FollowRecordId == id);
            if (followRecord == null)
            {
                return NotFound();
            }
            ViewData["FollowUserId"] = new SelectList(_context.Users, "Id", "UserName", followRecord.FollowUserId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", followRecord.UserId);
            return View(followRecord);
        }

        // POST: Admin/FollowRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FollowRecordId,UserId,FollowUserId,AddTime")] FollowRecord followRecord)
        {
            if (id != followRecord.FollowRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(followRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowRecordExists(followRecord.FollowRecordId))
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
            ViewData["FollowUserId"] = new SelectList(_context.Users, "Id", "UserName", followRecord.FollowUserId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", followRecord.UserId);
            return View(followRecord);
        }

        // GET: Admin/FollowRecord/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
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

        // POST: Admin/FollowRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var followRecord = await _context.FollowRecords.SingleOrDefaultAsync(m => m.FollowRecordId == id);
            _context.FollowRecords.Remove(followRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowRecordExists(string id)
        {
            return _context.FollowRecords.Any(e => e.FollowRecordId == id);
        }
    }
}
