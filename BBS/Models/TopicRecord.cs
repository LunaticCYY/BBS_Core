using System;

namespace BBS.Models
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
