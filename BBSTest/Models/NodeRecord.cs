using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSTest.Models
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
