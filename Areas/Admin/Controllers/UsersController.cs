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
    public class UsersController : Controller
    {
        private readonly HostelContext _context;

        public UsersController(HostelContext context)
        {
            _context = context;
        }

        public IActionResult LockUnlock(int id) { 
            if(!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/users";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }
            var user = _context.TbUsers.Find(id);
            if (user != null)
            {
                user.IsLock = !user.IsLock;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeRole(int id, int roleId)
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/users";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }
            var user = _context.TbUsers.Find(id);
            if (user != null)
            {
                user.RoleId = roleId;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            if (!Functions.IsLogin())
            {
                Functions.message = "Bạn chưa đăng nhập";
                Functions.returnlink = "/admin/users";
                return Redirect("/Login");
            }
            if (Functions.userrole == 1)
            {
                Functions.message = "Bạn không có quyền truy cập";
                return Redirect("/Home");

            }
            var hostelContext = _context.TbUsers.Include(t => t.Role);
            return View(await hostelContext.ToListAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.Iduser == id);
            if (tbUser == null)
            {
                return NotFound();
            }

            return View(tbUser);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iduser,Name,Phone,Birth,Avatar,Introduce,Password,AvatarType,RoleId,IsLock")] TbUser tbUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId", tbUser.RoleId);
            return View(tbUser);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers.FindAsync(id);
            if (tbUser == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId", tbUser.RoleId);
            return View(tbUser);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iduser,Name,Phone,Birth,Avatar,Introduce,Password,AvatarType,RoleId,IsLock")] TbUser tbUser)
        {
            if (id != tbUser.Iduser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbUserExists(tbUser.Iduser))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId", tbUser.RoleId);
            return View(tbUser);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.Iduser == id);
            if (tbUser == null)
            {
                return NotFound();
            }

            return View(tbUser);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbUser = await _context.TbUsers.FindAsync(id);
            if (tbUser != null)
            {
                _context.TbUsers.Remove(tbUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbUserExists(int id)
        {
            return _context.TbUsers.Any(e => e.Iduser == id);
        }
    }
}
