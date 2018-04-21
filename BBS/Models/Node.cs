using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public enum IsParent
    {
        [Display(Name = "父节点")]
        Parent,
        [Display(Name = "子节点")]
        Child
    }
    public class Node
    {
        public string NodeId { get; set; }
        [Display(Name = "节点名")]
        public string Name { get; set; }
        [Display(Name = "用户Id")]
        public string UserId { get; set; }
        [Display(Name = "是否父节点")]
        public IsParent IsParent { get; set; }
        [Display(Name = "父节点Id")]
        public string ParentId { get; set; }
        [Display(Name = "新增时间")]
        public DateTime AddTime { get; set; }

        public User User { get; set; }
        public ICollection<NodeRecord> NodeRecords { get; set; }
    }
}
