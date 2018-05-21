using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BBS.Models;
using BBS.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using BBS.Models.TopicViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using BBS.Data;
using System.Collections.Generic;
using BBS.Models.HomeViewModels;

namespace BBS.Controllers
{
    public class HomeController : Controller
    {
        private ITopicOperation _topic;
        private IOperation<Node> _node;
        private IOperation<NodeRecord> _nodeRecord;
        private IOperation<TopicRecord> _topicRecord;
        private IOperation<FollowRecord> _followRecord;
        public UserManager<User> UserManager { get; }

        public HomeController(ITopicOperation topic, 
            IOperation<Node> node, 
            IOperation<NodeRecord> nodeRecord,
            IOperation<TopicRecord> topicRecord,
            IOperation<FollowRecord> followRecord,
            UserManager<User> userManager)
        {
            _topic = topic;
            _node = node;
            _nodeRecord = nodeRecord;
            _topicRecord = topicRecord;
            _followRecord = followRecord;
            UserManager = userManager;
        }

        public IActionResult Index([FromServices]IUserServices userServices)
        {
            var pageSize = 50;
            var pageIndex = 1;
            Page<Topic> topicResult = null;
            if (!string.IsNullOrEmpty(Request.Query["page"]))
            {
                pageIndex = Convert.ToInt32(Request.Query["page"]);
            }
            if (!string.IsNullOrEmpty(Request.Query["s"]))
            {
                topicResult = _topic.PageList(a => a.Title.Contains(Request.Query["s"]), pageSize, pageIndex);
            }
            else
            {
                topicResult = _topic.PageList(pageSize, pageIndex);
            }
            ViewBag.Topics = topicResult.List.Select(a => new TopicViewModel
            {
                TopicId = a.TopicId,
                Title = a.Title,
                Image = a.User.Image,
                NodeId = a.NodeId,
                NodeName = a.Node.Name,
                UserId = a.UserId,
                UserName = a.User.UserName,
                LastReplyUserId = a.LastReplyUserId,
                LastReplyUserName = a.LastReplyUser == null ? string.Empty : a.LastReplyUser.UserName,
                ReplyCount = a.ReplyCount,
                LastReplyTime = a.LastReplyTime,
                AddTime = a.AddTime,
                LastTime = a.LastTime
            }).ToList();
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = topicResult.GetPageCount();
            var User = userServices.User.Result;
            ViewBag.User = User;
            if(User != null)
            {
                ViewBag.NodeRecordCount = _nodeRecord.TList(a => a.UserId == User.Id).ToList().Count();
                ViewBag.TopicRecordCount = _topicRecord.TList(a => a.UserId == User.Id).ToList().Count();
                ViewBag.FollowRecordCount = _followRecord.TList(a => a.UserId == User.Id).ToList().Count();
            }
            var nodes = _node.TList().ToList();
            ViewBag.Nodes = nodes;
            ViewBag.NodeListItem = nodes.Select(a => new SelectListItem { Value = a.NodeId, Text = a.Name });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Topic model)
        {
            if (ModelState.IsValid)
            {
                model.ViewCount = 0;
                model.ReplyCount = 0;
                model.AddTime = DateTime.Now;
                _topic.Add(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNodeRecord([Bind("UserId,NodeIdList")]NodeRecordModel nodeRecordModel)
        {
            if (ModelState.IsValid)
            {
                foreach(string nodeId in nodeRecordModel.NodeIdList)
                {
                    var nodeRecordCheck = _nodeRecord.TList(a => a.UserId == nodeRecordModel.UserId).Where(a => a.NodeId == nodeId);
                    if(nodeRecordCheck == null || nodeRecordCheck.Count() == 0)
                    {
                        NodeRecord nodeRecord = new NodeRecord();
                        nodeRecord.UserId = nodeRecordModel.UserId;
                        nodeRecord.NodeId = nodeId;
                        nodeRecord.AddTime = DateTime.Now;
                        _nodeRecord.Add(nodeRecord);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [Route("Home/Node/{nodeId}")]
        public IActionResult Node(string nodeId)
        {
            if (string.IsNullOrEmpty(nodeId))
            {
                return RedirectToAction("Index");
            }

            var pageSize = 50;
            var pageIndex = 1;
            Page<Topic> topicResult = null;
            if (!string.IsNullOrEmpty(Request.Query["page"]))
            {
                pageIndex = Convert.ToInt32(Request.Query["page"]);
            }
            if (!string.IsNullOrEmpty(Request.Query["s"]))
            {
                topicResult = _topic.PageList(a => a.Title.Contains(Request.Query["s"]), pageSize, pageIndex);
            }
            else
            {
                topicResult = _topic.PageList(a => a.NodeId == nodeId, pageSize, pageIndex);
            }
            ViewBag.Topics = topicResult.List.Select(a => new TopicViewModel
            {
                TopicId = a.TopicId,
                Title = a.Title,
                Image = a.User.Image,
                NodeId = a.NodeId,
                NodeName = a.Node.Name,
                UserId = a.UserId,
                UserName = a.User.UserName,
                LastReplyUserId = a.LastReplyUserId,
                LastReplyUserName = a.LastReplyUser == null ? string.Empty : a.LastReplyUser.UserName,
                ReplyCount = a.ReplyCount,
                LastReplyTime = a.LastReplyTime,
                AddTime = a.AddTime,
                LastTime = a.LastTime
            }).ToList();
            ViewBag.Count = topicResult.Total;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = topicResult.GetPageCount();
            var userId = UserManager.GetUserId(User);
            if (userId != null)
            {
                ViewBag.UserId = userId;
                ViewBag.NodeRecordCount = _nodeRecord.TList(a => a.UserId == userId).ToList().Count();
                ViewBag.TopicRecordCount = _topicRecord.TList(a => a.UserId == userId).ToList().Count();
                ViewBag.FollowRecordCount = _followRecord.TList(a => a.UserId == userId).ToList().Count();
            }
            var node = _node.GetById(nodeId);
            ViewBag.Node = node;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "在线论坛系统";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "联系方式";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
