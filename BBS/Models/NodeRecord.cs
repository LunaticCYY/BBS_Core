using System;

namespace BBS.Models
{
    public class NodeRecord
    {
        public string NodeRecordId { get; set; }
        public string UserId { get; set; }
        public string NodeId { get; set; }
        public DateTime AddTime { get; set; }

        //public User User { get; set; }
        //public Node Node { get; set; }
    }
}
