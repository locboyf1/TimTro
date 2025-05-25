using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimTro.Utilities;
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
            var blog = _context.TbBlogs.Include(i => i.TbBlogComments).Include(i=>i.IduserNavigation).Where(o=>o.IsShow).ToList();
            return await Task.FromResult<IActionResult>(View(blog));
        }

        [Route("blogs/blog_{id}.html")]
        public async Task<IActionResult> detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var blog = _context.TbBlogs.Include(i => i.TbBlogComments).ThenInclude(i=>i.IduserNavigation).Include(i => i.IduserNavigation).FirstOrDefault(i=>i.Idblog == id);
            if(blog == null)
            {
                return NotFound();
            }
            return await Task.FromResult<IActionResult>( View(blog));
        }

        [HttpPost]
        public async Task<IActionResult> Comment(int Idblog, bool IsChildComment, string Comment, int? IdcommentParent = null)
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = $"/blogs/blog_{Idblog}.html";
                return Redirect("/login");
            }
            else
            {
                TbBlogComment comment = new TbBlogComment
                {
                    Idblog = Idblog,
                    IsChildComment = IsChildComment,
                    IdcommentParent = IdcommentParent,
                    Comment = Comment,
                    Iduser = Functions.userid,
                    Time = DateTime.Now
                };
                _context.TbBlogComments.Add(comment);
                await _context.SaveChangesAsync();
                return Redirect($"/blogs/blog_{Idblog}.html");
            }
        }

    }
}
