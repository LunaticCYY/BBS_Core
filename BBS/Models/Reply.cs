using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public enum TopicOrReply
    {
        [Display(Name = "话题")]
        Topic,
        [Display(Name = "回复")]
        Reply
    }
    public class Reply
    {
        public string ReplyId { get; set; }
        [Display(Name = "用户Id")]
        public string UserId { get; set; }
        [Display(Name = "话题Id")]
        public string TopicId { get; set; }
        [Display(Name = "话题或评论")]
        public TopicOrReply? IsTopic { get; set; }
        [Display(Name = "回复Id")]
        public string ParentId { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "新增时间")]
        public DateTime AddTime { get; set; }
        [Display(Name = "最后修改时间")]
        public DateTime LastTime { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
