using System;

namespace BBS.Models
{
    public class FollowRecord
    {
        public int FollowRecordId { get; set; }
        public int UserId { get; set; }
        public int FollowId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
    }
}
