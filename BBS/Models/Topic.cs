using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class Topic
    {
        public string TopicId { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string NodeId { get; set; }
        public string LastReplyUserId { get; set; }
        public int ViewCount { get; set; }
        public int ReplyCount { get; set; }
        public DateTime LastReplyTime { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime LastTime { get; set; }

        public User User { get; set; }
        public Node Node { get; set; }
        [NotMapped]
        public User LastReplyUser { get; set; }
        public ICollection<TopicRecord> TopicRecords { get; set; }
        public ICollection<Reply> Replys { get; set; }
    }
}
