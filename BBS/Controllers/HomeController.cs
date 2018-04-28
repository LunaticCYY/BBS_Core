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
            ViewBag.NodeRecordCount = _nodeRecord.TList(a => a.UserId == User.Id).ToList().Count();
            ViewBag.TopicRecordCount = _topicRecord.TList(a => a.UserId == User.Id).ToList().Count();
            ViewBag.FollowRecordCount = _followRecord.TList(a => a.UserId == User.Id).ToList().Count();
            var nodes = _node.TList().ToList();
            ViewBag.Nodes = nodes;
            ViewBag.NodeListItem = nodes.Select(a => new SelectListItem { Value = a.NodeId, Text = a.Name });
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
