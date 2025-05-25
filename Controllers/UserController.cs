using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimTro.Models;
using TimTro.Utilities;

namespace TimTro.Controllers
{
    public class UserController : Controller
    {
        int pageSize = 5;
        private readonly HostelContext _context;

        public UserController(HostelContext context)
        {
            _context = context;
        }

        public IActionResult Me()
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/user/me";
                return Redirect("/Login");
            }
            var me = _context.TbUsers.FirstOrDefault(i=>i.Iduser == Functions.userid);
            return View(me);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TbUser user, IFormFile img)
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/user/me";
                return Redirect("/Login");
            }

            var me = await _context.TbUsers.FindAsync(Functions.userid);

            if (img != null && img.Length > 0)
            {
                me.Avatar = await Images.ConvertToByte(img);
                me.AvatarType = await Images.getImageType(img);

            }

            me.Birth = user.Birth;
            me.Introduce = user.Introduce;
            me.Name = user.Name;
            me.Phone = user.Phone;
            await _context.SaveChangesAsync();
            Functions.useravatar = me.Avatar;
            Functions.useravatartype = me.AvatarType;
            Functions.username = me.Name;
            Functions.userphone = me.Phone;
            Functions.message = "Cập nhật thành công";

            return await Task.FromResult<IActionResult>(RedirectToAction("Index", "Home"));
        }

        [Route("user/{id}.html")]
        public async Task<IActionResult> Index(int id, int page = 1)
        {
            var item = _context.TbUsers.Include(i => i.TbHostels).FirstOrDefault(i => i.Iduser == id);


            ViewBag.page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)item.TbHostels.ToList().Count / pageSize);
            return await Task.FromResult<IActionResult>(View(item));
        }
    }
}
