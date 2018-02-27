using System;
using System.Collections.Generic;

namespace BBS.Models
{
    public enum State
    {
        Online,
        Offline
    }
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Introduce { get; set; }
        public DateTime AddTime { get; set; }
        public State State { get; set; }

        public ICollection<Node> Nodes { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Reply> Replys { get; set; }
        public ICollection<NodeRecord> NodeRecords { get; set; }
        public ICollection<TopicRecord> TopicRecords { get; set; }
        public ICollection<FollowRecord> FollowRecords { get; set; }
    }
}
