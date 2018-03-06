using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} 必须大于{2}小于{1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "重置密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "重复密码")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "验证码")]
        public string Code { get; set; }
    }
}
