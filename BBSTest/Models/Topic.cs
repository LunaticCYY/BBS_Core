using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSTest.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int NodeId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public Node Node { get; set; }
        public ICollection<TopicRecord> TopicRecords { get; set; }
        public ICollection<Reply> Replys { get; set; }
    }
}
