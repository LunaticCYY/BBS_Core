using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSTest.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public int ParentId { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
