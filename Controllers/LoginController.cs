using Microsoft.AspNetCore.Mvc;
using TimTro.Models;
using TimTro.Utilities;

namespace TimTro.Controllers
{
    public class LoginController : Controller
    {

        private readonly HostelContext _context;

        public LoginController(HostelContext context)
        {
            _context = context;
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
                Functions.usermessage = "Sai số điện thoại hoặc mật khẩu";
                return View("Index");

            }
            else
            {
                Functions.userid = check.Iduser;
                Functions.userphone = check.Phone;
                Functions.usermessage = string.Empty;
                Functions.username = check.Name;
                Functions.useravatar = check.Avatar;
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
            string md5pass = Functions.MD5Password(password);
            var check = _context.TbUsers.FirstOrDefault(i => i.Phone == phone);
            if (check != null)
            {
                Functions.usermessage = "Số điện thoại đã được đăng ký";
                return View("Index");
            }
            else
            {
                TbUser user = new TbUser()
                {
                    Phone = phone,
                    Name = name,
                    Password = md5pass
                };

                _context.TbUsers.Add(user);
                _context.SaveChanges();
                Functions.usermessage = string.Empty;
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            Functions.username = string.Empty;
            Functions.userid = 0;
            Functions.userphone = string.Empty;
            Functions.usermessage = string.Empty;
            Functions.useravatar = string.Empty;
            return View("Index");
        }


    }
}
