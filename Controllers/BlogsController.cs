using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimTro.Models;

namespace TimTro.Controllers
{
    public class BlogsController : Controller
    {
        private readonly HostelContext _context;

        public BlogsController(HostelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blog = _context.TbBlogs.Include(i => i.TbBlogComments).Include(i=>i.IdadminNavigation).Where(o=>o.IsShow).ToList();
            return await Task.FromResult<IActionResult>(View(blog));
        }

        [Route("blogs/blog_{id}.html")]
        public async Task<IActionResult> detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var blog = _context.TbBlogs.Include(i => i.TbBlogComments).ThenInclude(i=>i.IduserNavigation).Include(i => i.IdadminNavigation).FirstOrDefault(i=>i.Idblog == id);
            if(blog == null)
            {
                return NotFound();
            }
            return await Task.FromResult<IActionResult>( View(blog));
        }
                
    

    }
}
