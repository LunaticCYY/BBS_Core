using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSTest.Models
{
    public class TopicRecord
    {
        public int TopicRecordId { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
