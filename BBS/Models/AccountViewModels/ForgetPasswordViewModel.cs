using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Models.AccountViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "注册邮箱")]
        public string Email { get; set; }
    }
}
