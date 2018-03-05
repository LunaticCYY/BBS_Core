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
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} 必须大于{2}小于{1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "重复密码")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
