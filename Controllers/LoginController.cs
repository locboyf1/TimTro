using elFinder.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.IO;
using TimTro.Models;
using TimTro.Utilities;

namespace TimTro.Controllers
{
    public class LoginController : Controller
    {

        private readonly HostelContext _context;
        private readonly IWebHostEnvironment _env;

        public LoginController(HostelContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string phone, string password)
        {
            string md5pass = Functions.MD5Password(password);
            var check = _context.TbUsers.FirstOrDefault(i => i.Phone == phone && i.Password == md5pass);
            if (check == null)
            {
                Functions.message = "Sai số điện thoại hoặc mật khẩu";
                return Redirect("/login");

            }
            else if (check.IsLock) {
                Functions.message = "Tài khoản bạn đã bị khóa";
                return Redirect("/login");
            }
            else
            {
                Functions.userrole = check.RoleId;
                Functions.userid = check.Iduser;
                Functions.userphone = check.Phone;
                Functions.message = string.Empty;
                Functions.username = check.Name;
                Functions.useravatar = check.Avatar;
                Functions.useravatartype = check.AvatarType;
                if (string.IsNullOrEmpty(Functions.returnlink))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(Functions.returnlink);
                }
            }

        }

        public IActionResult Register(string phone, string name, string password)
        {
            var relativePath = Path.Combine("assets/img/", "default-avatar.jpg");
            var absolutePath = Path.Combine(_env.WebRootPath, relativePath);
            byte[] imageBytes = System.IO.File.ReadAllBytes(absolutePath);

            string md5pass = Functions.MD5Password(password);
            var check = _context.TbUsers.FirstOrDefault(i => i.Phone == phone);
            if (check != null)
            {
                Functions.message = "Số điện thoại đã được đăng ký";
                return View("Index");
            }
            else
            {
                TbUser user = new TbUser()
                {
                    RoleId = 1,
                    Phone = phone,
                    Name = name,
                    Password = md5pass,
                    IsLock = false,
                    Avatar = imageBytes,
                    AvatarType = "image/jpeg"
                };

                _context.TbUsers.Add(user);
                _context.SaveChanges();
                Functions.message = string.Empty;
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            Functions.username = string.Empty;
            Functions.userid = 0;
            Functions.userphone = string.Empty;
            Functions.message = string.Empty;
            Functions.useravatar = null;
            Functions.useravatartype = string.Empty;
            Functions.userrole = 0;
            return RedirectToAction("Index", "Home");
        }


    }
}
