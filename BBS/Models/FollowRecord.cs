using System;

namespace BBS.Models
{
    public class FollowRecord
    {
        public string FollowRecordId { get; set; }
        public string UserId { get; set; }
        public string FollowId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
    }
}
