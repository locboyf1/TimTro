using Microsoft.AspNetCore.Mvc;
using TimTro.Utilities;

namespace TimTro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
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
            return View();
        }

    }
}
