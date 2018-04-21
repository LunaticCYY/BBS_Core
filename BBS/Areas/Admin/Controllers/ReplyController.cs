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
    public class ReplyController : Controller
    {
        private readonly BBSContext _context;

        public ReplyController(BBSContext context)
        {
            _context = context;
        }

        // GET: Admin/Reply
        public async Task<IActionResult> Index()
        {
            var bBSContext = _context.Replys.Include(r => r.Topic).Include(r => r.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/Reply/Details/5
        public async Task<IActionResult> Details(string id)
        {
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

        // GET: Admin/Reply/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/Reply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReplyId,UserId,TopicId,IsTopic,ParentId,Content,AddTime,LastTime")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                reply.AddTime = DateTime.Now;
                reply.LastTime = DateTime.Now;
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", reply.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reply.UserId);
            return View(reply);
        }

        // GET: Admin/Reply/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replys.SingleOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", reply.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reply.UserId);
            return View(reply);
        }

        // POST: Admin/Reply/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReplyId,UserId,TopicId,IsTopic,ParentId,Content,AddTime,LastTime")] Reply reply)
        {
            if (id != reply.ReplyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reply.LastTime = DateTime.Now;
                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.ReplyId))
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
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", reply.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", reply.UserId);
            return View(reply);
        }

        // GET: Admin/Reply/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
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
            var reply = await _context.Replys.SingleOrDefaultAsync(m => m.ReplyId == id);
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
