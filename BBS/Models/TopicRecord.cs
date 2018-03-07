using System;

namespace BBS.Models
{
    public class TopicRecord
    {
        public string TopicRecordId { get; set; }
        public string UserId { get; set; }
        public string TopicId { get; set; }
        public DateTime AddTime { get; set; }

        //public User User { get; set; }
        //public Topic Topic { get; set; }
    }
}
