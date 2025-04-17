using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimTro.Models;

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

        [Route("user/{id}")]
        public async Task<IActionResult> Index(int id, int page = 1)
        {
            var item = _context.TbUsers.Include(i => i.TbHostels).ThenInclude(i=>i.TbImageHostels).FirstOrDefault(i=>i.Iduser == id);


            ViewBag.page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)item.TbHostels.ToList().Count / pageSize);
            return await Task.FromResult<IActionResult>(View(item));
        }
    }
}
