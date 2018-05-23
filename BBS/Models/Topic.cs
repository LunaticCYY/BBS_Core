using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class Topic
    {
        public string TopicId { get; set; }
        [Display(Name = "标题")]
        [StringLength(100, ErrorMessage = "{0}长度应小于{1}")]
        public string Title { get; set; }
        [Display(Name = "创建人Id")]
        public string UserId { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "节点Id")]
        public string NodeId { get; set; }
        [Display(Name = "最后回复用户Id")]
        public string LastReplyUserId { get; set; }
        [Display(Name = "浏览数")]
        public int ViewCount { get; set; }
        [Display(Name = "回复数")]
        public int ReplyCount { get; set; }
        [Display(Name = "最后回复时间")]
        public DateTime LastReplyTime { get; set; }
        [Display(Name = "新增时间")]
        public DateTime AddTime { get; set; }
        [Display(Name = "最后修改时间")]
        public DateTime LastTime { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Topics")]
        public User User { get; set; }
        public Node Node { get; set; }
        [ForeignKey("LastReplyUserId")]
        [InverseProperty("LastUserTopics")]
        public User LastReplyUser { get; set; }
        public ICollection<TopicRecord> TopicRecords { get; set; }
        public ICollection<Reply> Replys { get; set; }
    }
}
