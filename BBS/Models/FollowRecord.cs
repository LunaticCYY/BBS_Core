using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class FollowRecord
    {
        public string FollowRecordId { get; set; }
        public string UserId { get; set; }
        public string FollowUserId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        [NotMapped]
        public User FollowUser { get; set; }
    }
}
