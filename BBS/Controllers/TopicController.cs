using BBS.Models;
using BBS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BBS.Controllers
{
    public class TopicController : Controller
    {
        private ITopicOperation _topic;
        private IOperation<Node> _node;
        private IReplyOperation _reply;

        public TopicController(ITopicOperation topic, IOperation<Node> node, IReplyOperation reply)
        {
            _topic = topic;
            _node = node;
            _reply = reply;
        }

        [Route("Topic/{id}")]
        public IActionResult Index(string topicId)
        {
            var topic = _topic.GetById(topicId);
            if(topic == null)
            {
                return Redirect("/");
            }
            var replys = _reply.TList(a => a.TopicId == topicId).ToList();
            topic.ViewCount += 1;
            _topic.Update(topic);
            ViewBag.Replys = replys;
            return View(topic);
        }
    }
}