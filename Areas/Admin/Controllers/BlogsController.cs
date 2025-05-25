using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimTro.Models;
using TimTro.Utilities;

namespace TimTro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly HostelContext _context;

        public BlogsController(HostelContext context)
        {
            _context = context;
        }

        // GET: Admin/Blogs
        public async Task<IActionResult> Index()
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/blogs";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/home");

            }
            var hostelContext = _context.TbBlogs.Include(t => t.IduserNavigation);
            return View(await hostelContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult HideShow(int id)
        {
            var blog = _context.TbBlogs.Find(id);
            if (blog != null)
            {
                blog.IsShow = !blog.IsShow;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/blogs";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/home");

            }
            if (id == null)
            {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs
                .Include(t => t.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idblog == id);
            if (tbBlog == null)
            {
                return NotFound();
            }

            return View(tbBlog);
        }

        // GET: Admin/Blogs/Create
        public IActionResult Create()
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/blogs/create";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/home");

            }
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idblog,Title,Idadmin,Time,Description,Detail,IsShow")] TbBlog tbBlog, IFormFile image)
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/blogs";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/home");

            }
            tbBlog.Iduser = Functions.userid;
            tbBlog.Time = DateTime.Now;

            tbBlog.Image = await Images.ConvertToByte(image);
            tbBlog.ImageType = await Images.getImageType(image);

            if (ModelState.IsValid)
            {
                _context.Add(tbBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Idadmin"] = new SelectList(_context.TbAdmins, "Idadmin", "Idadmin", tbBlog.Idadmin);
            return View(tbBlog);
        }

        // GET: Admin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = $"/admin/blogs/edit/{id}";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/home");

            }
            if (id == null)
            {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs.FindAsync(id);
            if (tbBlog == null)
            {
                return NotFound();
            }


            return View(tbBlog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idblog,Title,Idadmin,Time,Description,Detail,Image,IsShow,ImageType")] TbBlog tbBlog, IFormFile img)
        {
            if (id != tbBlog.Idblog)
            {
                return NotFound();
            }

            var blog = await _context.TbBlogs.FindAsync(id);

            if (img != null && img.Length > 0)
            {
                blog.Image = await Images.ConvertToByte(img);
                blog.ImageType = await Images.getImageType(img);
            }

            blog.Description = tbBlog.Description;
            blog.Detail = tbBlog.Detail;
            blog.IsShow = tbBlog.IsShow;
            blog.Title = tbBlog.Title;

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        private bool TbBlogExists(int id)
        {
            return _context.TbBlogs.Any(e => e.Idblog == id);
        }
    }
}
