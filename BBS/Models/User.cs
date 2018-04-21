using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public enum State
    {
        [Display(Name = "在线")]
        Online,
        [Display(Name = "离线")]
        Offline
    }
    public class User : IdentityUser
    {
        [Display(Name = "头像")]
        public string Image { get; set; }
        [Display(Name = "自我介绍")]
        public string Introduce { get; set; }
        [Display(Name = "加入时间")]
        public DateTime AddTime { get; set; }
        [Display(Name = "上次登录时间")]
        public DateTime LastTime { get; set; }
        [Display(Name = "状态")]
        public State? State { get; set; }

        public ICollection<Node> Nodes { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Topic> LastUserTopics { get; set; }
        public ICollection<Reply> Replys { get; set; }
        public ICollection<NodeRecord> NodeRecords { get; set; }
        public ICollection<TopicRecord> TopicRecords { get; set; }
        public ICollection<FollowRecord> FollowRecords { get; set; }
        public ICollection<FollowRecord> FollowUserRecords { get; set; }
    }
}
