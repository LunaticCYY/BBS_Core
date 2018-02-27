using System;

namespace BBS.Models
{
    public class NodeRecord
    {
        public int NodeRecordId { get; set; }
        public int UserId { get; set; }
        public int NodeId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public Node Node { get; set; }
    }
}
