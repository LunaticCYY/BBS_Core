using BBS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "确定注册邮箱", $"请单击该链接确定您的账户：<a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
