using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class NodeRecord
    {
        public string NodeRecordId { get; set; }
        [Display(Name = "收藏人Id")]
        public string UserId { get; set; }
        [Display(Name = "节点Id")]
        public string NodeId { get; set; }
        [Display(Name = "新增时间")]
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public Node Node { get; set; }
    }
}
