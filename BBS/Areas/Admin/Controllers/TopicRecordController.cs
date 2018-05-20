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
    public class TopicRecordController : Controller
    {
        private readonly BBSContext _context;

        public TopicRecordController(BBSContext context)
        {
            _context = context;
        }

        // GET: Admin/TopicRecord
        public async Task<IActionResult> Index()
        {
            var bBSContext = _context.TopicRecords.Include(t => t.Topic).Include(t => t.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/TopicRecord/Details/5
        public async Task<IActionResult> Details(string id)
        {
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

        // GET: Admin/TopicRecord/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/TopicRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicRecordId,UserId,TopicId,AddTime")] TopicRecord topicRecord)
        {
            if (ModelState.IsValid)
            {
                topicRecord.AddTime = DateTime.Now;
                _context.Add(topicRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", topicRecord.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", topicRecord.UserId);
            return View(topicRecord);
        }

        // GET: Admin/TopicRecord/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicRecord = await _context.TopicRecords.SingleOrDefaultAsync(m => m.TopicRecordId == id);
            if (topicRecord == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", topicRecord.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", topicRecord.UserId);
            return View(topicRecord);
        }

        // POST: Admin/TopicRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TopicRecordId,UserId,TopicId,AddTime")] TopicRecord topicRecord)
        {
            if (id != topicRecord.TopicRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicRecordExists(topicRecord.TopicRecordId))
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
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", topicRecord.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", topicRecord.UserId);
            return View(topicRecord);
        }

        // GET: Admin/TopicRecord/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
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

        // POST: Admin/TopicRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var topicRecord = await _context.TopicRecords.Include(t => t.Topic).Include(t => t.User).SingleOrDefaultAsync(m => m.TopicRecordId == id);
            topicRecord.User = null;
            topicRecord.Topic = null;
            _context.TopicRecords.Remove(topicRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicRecordExists(string id)
        {
            return _context.TopicRecords.Any(e => e.TopicRecordId == id);
        }
    }
}
