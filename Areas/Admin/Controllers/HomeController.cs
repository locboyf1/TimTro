using Microsoft.AspNetCore.Mvc;
using TimTro.Utilities;
using Microsoft.EntityFrameworkCore;
using TimTro.Models;

namespace TimTro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly HostelContext _context;

        public HomeController(HostelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Approval(int id)
        {
            if(!Functions.IsLogin())
            {
                return Redirect($"/hostel/detail/{id}.html");
            }
            if (Functions.userrole != 2)
            {
                return Redirect($"/hostel/detail/{id}.html");
            }

            var hostel = await _context.TbHostels.FindAsync(id);
            if (hostel == null)
            {
                return NotFound();
            }
            hostel.IsApproval = !hostel.IsApproval;
            await _context.SaveChangesAsync();
            return Redirect($"/hostel/{id}.html");
        }

        public IActionResult Index()
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/Home");

            }

            var NewHostels = _context.TbHostels.Where(i=>i.IsApproval == false).OrderByDescending(i => i.UploadDate).Include(i=>i.IduserNavigation).ToList();

            ViewBag.hostels = _context.TbHostels.ToList();
            ViewBag.users = _context.TbUsers.ToList();

            return View(NewHostels);
        }

    }
}
