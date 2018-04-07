﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Models.AccountViewModels
{
    public class ChangeUserInfoViewModel
    {
        [Display(Name = "用户名")]
        [StringLength(20, ErrorMessage = "{0}长度至少大于{2}小于{1}", MinimumLength = 3)]
        public string UserName { get; set; }
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "手机号码")]
        public string PhoneNumber { get; set; }
        [Display(Name = "头像")]
        public string Image { get; set; }
        [Display(Name = "自我介绍")]
        public string Introduce { get; set; }
    }
}
