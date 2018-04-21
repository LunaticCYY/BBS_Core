using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class FollowRecord
    {
        public string FollowRecordId { get; set; }
        [Display(Name = "关注人Id")]
        public string UserId { get; set; }
        [Display(Name = "被关注人Id")]
        public string FollowUserId { get; set; }
        [Display(Name = "关注时间")]
        public DateTime AddTime { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("FollowRecords")]
        public User User { get; set; }
        [ForeignKey("FollowUserId")]
        [InverseProperty("FollowUserRecords")]
        public User FollowUser { get; set; }
    }
}
