using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class TopicRecord
    {
        public string TopicRecordId { get; set; }
        [Display(Name = "收藏人")]
        public string UserId { get; set; }
        [Display(Name = "话题")]
        public string TopicId { get; set; }
        [Display(Name = "新增时间")]
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
