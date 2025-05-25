using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SixLabors.ImageSharp;
using TimTro.Models;
using TimTro.Utilities;

namespace TimTro.Controllers
{
    public class ListHostelsController : Controller
    {
        private readonly HostelContext _context;

        public ListHostelsController(HostelContext context)
        {
            _context = context;
        }

        // GET: TbHostels
        public async Task<IActionResult> Index()
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = "/listhostels";
                return Redirect("/login");
            }
            else
            {
                Functions.returnlink = string.Empty;

                var hostelContext = _context.TbHostels.Include(t => t.IduserNavigation).Where(i => i.Iduser == Functions.userid);
                return View(await hostelContext.ToListAsync());
            }
        }

        // GET: TbHostels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = "/listhostels/create";
                return Redirect("/login");
            }
            Functions.returnlink = string.Empty;

            if (id == null)
            {
                return NotFound();
            }

            var tbHostel = await _context.TbHostels
                .Include(t => t.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idhostel == id);
            if (tbHostel == null)
            {
                return NotFound();
            }

            return View(tbHostel);
        }

        // GET: TbHostels/Create
        public IActionResult Create()
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = "/listhostels/create";
                return Redirect("/login");
            }
            Functions.returnlink = string.Empty;

            return View();
        }

        // POST: TbHostels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idhostel,Title,Price,Iduser,Description,Area,IsPrivate,IsHasParkingLot,UploadDate,Ward,District,Province,AddressDetail,IsShow,IsApproval,IsAvailable")] TbHostel tbHostel, IFormFile img)
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = "/listhostels/create";
                return Redirect("/login");
            }
            Functions.returnlink = string.Empty;

            tbHostel.Iduser = Functions.userid;
            tbHostel.UploadDate = DateOnly.FromDateTime(DateTime.Now);
            tbHostel.IsAvailable = true;

            tbHostel.Image = await Images.ConvertToByte(img);
            tbHostel.ImageType = await Images.getImageType(img);

            _context.TbHostels.Add(tbHostel);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        // GET: TbHostels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = $"/listhostels/edit/{id}";
                return Redirect("/login");
            }
            Functions.returnlink = string.Empty;

            if (id == null)
            {
                return NotFound();
            }

            var tbHostel = await _context.TbHostels.FirstOrDefaultAsync(i => i.Idhostel == id);
            if (tbHostel == null)
            {
                return NotFound();
            }

            return View(tbHostel);
        }

        // POST: TbHostels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idhostel,Title,Price,Iduser,Description,Area,IsPrivate,IsHasParkingLot,UploadDate,Ward,District,Province,AddressDetail,IsShow,IsApproval,IsAvailable")] TbHostel tbHostel, IFormFile img)
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = $"/listhostels/edit/{id}";
                return Redirect("/login");
            }
            Functions.returnlink = string.Empty;
            if (id != tbHostel.Idhostel)
                return NotFound();


            var hostelInDb = await _context.TbHostels.FindAsync(id);
            if (hostelInDb == null)
            {
                return NotFound();
            }

            hostelInDb.Title = tbHostel.Title;
            hostelInDb.Price = tbHostel.Price;
            hostelInDb.Iduser = tbHostel.Iduser;
            hostelInDb.Description = tbHostel.Description;
            hostelInDb.Area = tbHostel.Area;
            hostelInDb.IsPrivate = tbHostel.IsPrivate;
            hostelInDb.IsHasParkingLot = tbHostel.IsHasParkingLot;
            hostelInDb.UploadDate = tbHostel.UploadDate;
            hostelInDb.Ward = tbHostel.Ward;
            hostelInDb.District = tbHostel.District;
            hostelInDb.Province = tbHostel.Province;
            hostelInDb.AddressDetail = tbHostel.AddressDetail;
            hostelInDb.IsShow = tbHostel.IsShow;
            hostelInDb.IsApproval = tbHostel.IsApproval;
            hostelInDb.IsAvailable = tbHostel.IsAvailable;


            if (img != null && img.Length > 0)
            {
                hostelInDb.Image = await Images.ConvertToByte(img);
                hostelInDb.ImageType = await Images.getImageType(img);
            }

            //_context.Update(tbHostel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }


        // GET: TbHostels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = $"/listhostels/delete/{id}";
                return Redirect("/login");
            }
            Functions.returnlink = string.Empty;
            if (id == null)
            {
                return NotFound();
            }

            var tbHostel = await _context.TbHostels
                .Include(t => t.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idhostel == id);
            if (tbHostel == null)
            {
                return NotFound();
            }

            return View(tbHostel);
        }

        // POST: TbHostels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbHostel = await _context.TbHostels.FindAsync(id);
            if (tbHostel != null)
            {
                _context.TbHostels.Remove(tbHostel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbHostelExists(int id)
        {
            return _context.TbHostels.Any(e => e.Idhostel == id);
        }
    }
}
