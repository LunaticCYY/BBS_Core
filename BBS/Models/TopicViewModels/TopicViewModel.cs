using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Models.TopicViewModels
{
    public class TopicViewModel
    {
        public string TopicId { get; set; }
        public string Title { get; set; }
        public string NodeName { get; set; }
        public string Avator { get; set; }
        public string LastReplyUserName { get; set; }
        public int ReplyCount { get; set; }
        public DateTime LastReplyTime { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime LastTime { get; set; }
    }
}
