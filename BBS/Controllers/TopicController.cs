using Microsoft.AspNetCore.Mvc;

namespace BBS.Controllers
{
    public class TopicController : Controller
    {
        [Route("Topic/{id}")]
        public IActionResult Index(int topicId)
        {
            
            return View();
        }
    }
}