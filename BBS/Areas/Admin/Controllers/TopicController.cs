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
    public class TopicController : Controller
    {
        private readonly BBSContext _context;

        public TopicController(BBSContext context)
        {
            _context = context;
        }

        // GET: Admin/Topic
        public async Task<IActionResult> Index()
        {
            var bBSContext = _context.Topics.Include(t => t.LastReplyUser).Include(t => t.Node).Include(t => t.User);
            return View(await bBSContext.ToListAsync());
        }

        // GET: Admin/Topic/Details/5
        public async Task<IActionResult> Details(string id)
        {
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

        // GET: Admin/Topic/Create
        public IActionResult Create()
        {
            ViewData["LastReplyUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicId,Title,UserId,Content,NodeId,LastReplyUserId,ViewCount,ReplyCount,LastReplyTime,AddTime,LastTime")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.AddTime = DateTime.Now;
                topic.LastTime = DateTime.Now;
                topic.ViewCount = 0;
                topic.ReplyCount = 0;
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LastReplyUserId"] = new SelectList(_context.Users, "Id", "UserName", topic.LastReplyUserId);
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name", topic.NodeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", topic.UserId);
            return View(topic);
        }

        // GET: Admin/Topic/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.SingleOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }
            ViewData["LastReplyUserId"] = new SelectList(_context.Users, "Id", "UserName", topic.LastReplyUserId);
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name", topic.NodeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", topic.UserId);
            return View(topic);
        }

        // POST: Admin/Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TopicId,Title,UserId,Content,NodeId,LastReplyUserId,ViewCount,ReplyCount,LastReplyTime,AddTime,LastTime")] Topic topic)
        {
            if (id != topic.TopicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    topic.LastTime = DateTime.Now;
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.TopicId))
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
            ViewData["LastReplyUserId"] = new SelectList(_context.Users, "Id", "UserName", topic.LastReplyUserId);
            ViewData["NodeId"] = new SelectList(_context.Nodes, "NodeId", "Name", topic.NodeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", topic.UserId);
            return View(topic);
        }

        // GET: Admin/Topic/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
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
            var topic = await _context.Topics.SingleOrDefaultAsync(m => m.TopicId == id);
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
