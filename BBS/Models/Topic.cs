using System;
using System.Collections.Generic;

namespace BBS.Models
{
    public class Topic
    {
        public string TopicId { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string NodeId { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime LastTime { get; set; }

        //public User User { get; set; }
        //public Node Node { get; set; }
        //public ICollection<TopicRecord> TopicRecords { get; set; }
        //public ICollection<Reply> Replys { get; set; }
    }
}
