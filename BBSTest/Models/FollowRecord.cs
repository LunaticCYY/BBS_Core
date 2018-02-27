using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBSTest.Models
{
    public class FollowRecord
    {
        public int FollowRecordId { get; set; }
        public int UserId { get; set; }
        public int FollowId { get; set; }
        public DateTime AddTime { get; set; }

        public User User { get; set; }
    }
}
