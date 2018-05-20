using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Models.AccountViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0}长度至少大于{2}小于{1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "{0}长度至少大于{2}小于{1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "重复密码")]
        [Compare("NewPassword", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }
    }
}
