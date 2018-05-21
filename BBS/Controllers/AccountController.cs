using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using BBS.Models.AccountViewModels;
using BBS.Models;
using Microsoft.Extensions.Logging;
using BBS.Services;
using BBS.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BBS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly IEmailSender _emailSender;
        private IOperation<User> _user;
        //private readonly ILogger _logger;
        private IHostingEnvironment _hostingEnvironment;
        private ITopicOperation _topic;
        private IReplyOperation _reply;
        private INodeRecordOperation _nodeRecord;
        private ITopicRecordOperation _topicRecord;
        private IFollowRecordOperation _followRecord;

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            //IEmailSender emailSender,
            IOperation<User> user,
            //ILogger<AccountController> logger,
            IHostingEnvironment hostingEnvironment,
            ITopicOperation topic,
            IReplyOperation reply,
            INodeRecordOperation nodeRecord,
            ITopicRecordOperation topicRecord,
            IFollowRecordOperation followRecord
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            //_logger = logger;
            _user = user;
            _hostingEnvironment = hostingEnvironment;
            _topic = topic;
            _reply = reply;
            _nodeRecord = nodeRecord;
            _topicRecord = topicRecord;
            _followRecord = followRecord;
        }
        
        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("用户登录");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    //_logger.LogWarning("{ Email }登录失败.", model.Email);
                    ModelState.AddModelError(string.Empty, "用户名或密码错误");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ResultUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ResultUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var CheckEmail = _user.TList(a => a.Email == model.Email).ToList();
                if(CheckEmail != null && CheckEmail.Count > 0)
                {
                    ModelState.AddModelError(string.Empty, "该邮箱已被注册");
                    return View(model);
                }
                var user = new User { UserName = model.Email, Email = model.Email, EmailConfirmed = true, AddTime = DateTime.Now };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("成功创建用户.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation("已创建新用户和密码.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("注销当前账户.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ChangePassword([Bind("Password,NewPassword,ConfirmPassword")]ChangePasswordViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _user.GetById(userId);
            _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Account/UserInfo/{userId}")]
        public IActionResult UserInfo(string userId)
        {
            var user = _user.GetById(userId);
            var topics = _topic.TList(a => a.UserId == userId).OrderByDescending(a => a.AddTime).ToList();
            var replys = _reply.TList(a => a.UserId == userId).OrderByDescending(a => a.AddTime).ToList();
            ViewBag.TopicCount = topics.Count;
            ViewBag.ReplyCount = replys.Count;
            ViewBag.User = user;
            ViewBag.Topics = topics;
            ViewBag.Replys = replys;
            return View();
        }

        [Route("/Account/NodeRecord")]
        public IActionResult NodeRecord()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }

            var userId = _userManager.GetUserId(User);
            var nodeRecord = _nodeRecord.TList(a => a.UserId == userId).OrderByDescending(a => a.AddTime).ToList();
            return View(nodeRecord);
        }

        [Route("/Account/TopicRecord")]
        public IActionResult TopicRecord()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }

            var userId = _userManager.GetUserId(User);
            var topicRecord = _topicRecord.TList(a => a.UserId == userId).OrderByDescending(a => a.AddTime).ToList();
            return View(topicRecord);
        }

        [Route("/Account/FollowRecord")]
        public IActionResult FollowRecord()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }

            var userId = _userManager.GetUserId(User);
            var followRecord = _followRecord.TList(a => a.UserId == userId).OrderByDescending(a => a.AddTime).ToList();
            return View(followRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelNodeRecord([Bind("NodeRecordId,UserId,NodeId,AddTime")]NodeRecord nodeRecord)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }
            var nodeRecordBak = _nodeRecord.GetById(nodeRecord.NodeRecordId);

            nodeRecordBak.User = null;
            nodeRecordBak.Node = null;
            _nodeRecord.Delete(nodeRecordBak);
            return RedirectToAction("NodeRecord");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelTopicRecord([Bind("TopicRecordId,UserId,TopicId,AddTime")]TopicRecord topicRecord)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }

            var topicRecordBak = _topicRecord.GetById(topicRecord.TopicRecordId);
            topicRecordBak.User = null;
            topicRecordBak.Topic = null;
            _topicRecord.Delete(topicRecordBak);
            return RedirectToAction("TopicRecord");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelFollowRecord([Bind("FollowRecordId,UserId,,FollowUserId,AddTime")]FollowRecord followRecord)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login");
            }

            var followRecordBak = _followRecord.GetById(followRecord.FollowRecordId);
            followRecordBak.User = null;
            followRecordBak.FollowUser = null;
            _followRecord.Delete(followRecordBak);
            return RedirectToAction("FollowRecord");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                throw new ApplicationException($"无法获取当前用户Id'{_userManager.GetUserId(User)}'.");
            }

            var model = new ChangeUserInfoViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Introduce = user.Introduce
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserInfo(ChangeUserInfoViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"无法获取当前用户Id'{_userManager.GetUserId(User)}'.");
            }
            string imageSave = string.Empty;
            if(model.Image != null)
            {
                imageSave = SaveImage(model.Image);
            }
            
            if (!string.IsNullOrEmpty(model.UserName) && !user.UserName.Equals(model.UserName))
            {
                user.UserName = model.UserName;
                user.NormalizedUserName = model.UserName;
            }
            if (!string.IsNullOrEmpty(model.Email) && !user.Email.Equals(model.Email))
            {
                user.Email = model.Email;
                user.NormalizedEmail = model.Email;
            }
            if (!string.IsNullOrEmpty(model.PhoneNumber) && !user.PhoneNumber.Equals(model.PhoneNumber))
            {
                user.PhoneNumber = model.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(imageSave))
            {
                user.Image = imageSave;
            }
            if (!string.IsNullOrEmpty(model.Introduce))
            {
                user.Introduce = model.Introduce;
            }

            _user.Update(user);
            return RedirectToLocal(returnUrl);
        }

        #region Helpers
        private string SaveImage(IFormFile imageFile)
        {
            string filePath = _hostingEnvironment.WebRootPath + @"\UploadImages\";
            string fileSave = string.Empty;
            string imageName = string.Empty;

            if (imageFile.Length > 0)
            {
                string fileExt = imageFile.ContentType.Split("/")[1];
                imageName = Guid.NewGuid().ToString();
                fileSave = imageName + "." + fileExt;
                filePath += fileSave;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
            }

            return string.IsNullOrEmpty(fileSave) ? string.Empty : fileSave;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }



        #endregion
    }
}