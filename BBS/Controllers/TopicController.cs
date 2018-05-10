using BBS.Models;
using BBS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BBS.Controllers
{
    public class TopicController : Controller
    {
        private ITopicOperation _topic;
        private IOperation<Node> _node;
        private IOperation<TopicRecord> _topicRecord;
        private IOperation<FollowRecord> _followRecord;
        private IReplyOperation _reply;

        public UserManager<User> UserManager { get; }

        public TopicController(ITopicOperation topic,
            IOperation<Node> node,
            IOperation<TopicRecord> topicRecord, 
            IOperation<FollowRecord> followRecord,
            IReplyOperation reply,
            UserManager<User> userManager)
        {
            _topic = topic;
            _node = node;
            _topicRecord = topicRecord;
            _followRecord = followRecord;
            _reply = reply;
            UserManager = userManager;
        }

        [Route("Topic/{id}")]
        public IActionResult Index(string id)
        {
            var topic = _topic.GetById(id);
            if(topic == null)
            {
                return Redirect("/");
            }
            var replys = _reply.TList(a => a.TopicId == id).ToList();
            topic.ViewCount += 1;
            _topic.Update(topic);
            var topicRecord = _topicRecord.TList(a => a.TopicId == id);
            var followRecord = _followRecord.TList(a => a.FollowUserId == topic.UserId);
            var userId = UserManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userId))
            {
                topicRecord = topicRecord.Where(a => a.UserId == userId);
                followRecord = followRecord.Where(a => a.UserId == userId);
            }
            if(topicRecord.Count() <= 0)
            {
                ViewBag.IsTopicRecord = 0;
            }
            else
            {
                ViewBag.IsTopicRecord = 1;
            }
            if (followRecord.Count() <= 0)
            {
                ViewBag.IsFollowRecord = 0;
            }
            else
            {
                ViewBag.IsFollowRecord = 1;
            }
            ViewBag.Replys = replys;
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Topic/{id}")]
        public IActionResult Index([Bind("TopicId,UserId,Content")]Reply reply)
        {
            if(ModelState.IsValid && !string.IsNullOrEmpty(reply.Content))
            {
                reply.AddTime = DateTime.Now;
                reply.IsTopic = TopicOrReply.Topic;
                reply.LastTime = reply.AddTime;
                _reply.Add(reply);
                var topic = _topic.GetById(reply.TopicId);
                topic.ReplyCount += 1;
                topic.LastReplyUserId = reply.UserId;
                topic.LastReplyTime = reply.AddTime;
                _topic.Update(topic);
            }
            return RedirectToAction("Index", "Topic", new { Id = reply.TopicId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Topic/AddTopicRecord")]
        public IActionResult AddTopicRecord([Bind("TopicId, UserId")]TopicRecord topicRecord)
        {
            if (ModelState.IsValid)
            {
                topicRecord.AddTime = DateTime.Now;
                _topicRecord.Add(topicRecord);
            }
            return RedirectToAction("Index", "Topic", new { Id = topicRecord.TopicId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Topic/AddFollowRecord")]
        public IActionResult AddFollowRecord([Bind("FollowRecordId, FollowUserId, UserId")]FollowRecord followRecord)
        {
            var TopicId = followRecord.FollowRecordId;
            followRecord.FollowRecordId = null;
            if (ModelState.IsValid)
            {
                followRecord.AddTime = DateTime.Now;
                _followRecord.Add(followRecord);
            }
            return RedirectToAction("Index", "Topic", new { Id = TopicId });
        }
    }
}