using System;

namespace BBS.Models
{
    public class Reply
    {
        public string ReplyId { get; set; }
        public string UserId { get; set; }
        public string TopicId { get; set; }
        public string ParentId { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
