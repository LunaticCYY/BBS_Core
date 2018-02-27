using System;
using System.Collections.Generic;

namespace BBS.Models
{
    public enum IsParent
    {
        Parent,
        Child
    }
    public class Node
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public IsParent IsParent { get; set; }
        public int ParentId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public ICollection<NodeRecord> NodeRecords { get; set; }
    }
}
