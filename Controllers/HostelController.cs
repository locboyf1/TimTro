using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimTro.Configurations;
using TimTro.Models;
using TimTro.Utilities;

namespace TimTro.Controllers
{
    public class HostelController : Controller
    {
        private readonly string _mapboxToken;
        private readonly HostelContext _context;
        int pageSize = 10;
        public HostelController(HostelContext context, IOptions<MapboxSettings> mapboxSettings)
        {

            _context = context;
            _mapboxToken = mapboxSettings.Value.AccessToken;
        }

        public async Task<IActionResult> Index()
        {
            var item = _context.TbHostels.Where(i => i.IsApproval).Where(i => i.IsShow).OrderByDescending(i => i.UploadDate).Include(i => i.IduserNavigation).ToList();

            return await Task.FromResult<IActionResult>(View(item));
        }

        [HttpPost]
        public async Task<IActionResult> Index(string priceRange, string areaRange, int province, int district, string ward, int IsPrivate, int IsHasParkingLot)
        {
            int minArea = 0;
            int maxArea = 150;
            int minPrice = 0;
            int maxPrice = 8000000;

            // Bóc chuỗi dạng [500.000 - 3.000.000] VNĐ
            var match = Regex.Match(priceRange, @"\[(.*?)\]");
            if (match.Success)
            {
                var range = match.Groups[1].Value; // "500.000 - 3.000.000"
                var parts = range.Split('-');
                if (parts.Length == 2)
                {
                    // Bỏ khoảng trắng và chấm
                    string minStr = parts[0].Trim().Replace(".", "");
                    string maxStr = parts[1].Trim().Replace(".", "");

                    if (int.TryParse(minStr, out int min)) minPrice = min;
                    if (int.TryParse(maxStr, out int max)) maxPrice = max;
                }
            }

            match = Regex.Match(areaRange, @"\[(.*?)\]");
            if (match.Success)
            {
                var range = match.Groups[1].Value;
                var parts = range.Split('-');
                if (parts.Length == 2)
                {
                    string minStr = parts[0].Trim().Replace(".", "");
                    string maxStr = parts[1].Trim().Replace(".", "");

                    if (int.TryParse(minStr, out int min)) minArea = min;
                    if (int.TryParse(maxStr, out int max)) maxArea = max;
                }
            }

            var item = _context.TbHostels.Where(i => i.IsShow).Where(i => i.Price >= minPrice && i.Price <= maxPrice).Where(i => i.Area >= minArea && i.Area <= maxArea).Where(i => i.IsApproval).OrderByDescending(i => i.UploadDate).Include(i => i.IduserNavigation).AsQueryable();

            if (province != null && province != 0)
                item = item.Where(h => h.Province == province);

            if (district != null && district != 0)
                item = item.Where(h => h.District == district);

            if (!string.IsNullOrEmpty(ward))
                item = item.Where(h => h.Ward == ward);

            if (IsPrivate == 1)
                item = item.Where(h => h.IsPrivate == true);
            else if (IsPrivate == 2)
                item = item.Where(h => h.IsPrivate == false);

            if (IsHasParkingLot == 1)
                item = item.Where(h => h.IsHasParkingLot == true);
            else if (IsHasParkingLot == 2)
                item = item.Where(h => h.IsHasParkingLot == false);

            var hostel = item.ToList();

            return await Task.FromResult<IActionResult>(View(hostel));
        }

        [Route("Hostel/{id}.html")]
        public async Task<IActionResult> Detail(int id)
        {
            var hostel = await _context.TbHostels.Include(h => h.IduserNavigation).Include(i => i.TbReviews).FirstOrDefaultAsync(h => h.Idhostel == id);
            if (hostel == null)
            {
                return NotFound();
            }
            if ((!hostel.IsApproval || !hostel.IsShow) && Functions.userrole != 2 && hostel.Iduser != Functions.userid)
            {
                return NotFound();
            }
            ViewData["MapboxToken"] = _mapboxToken;
            return View(hostel);
        }

        [HttpPost]
        public async Task<IActionResult> Review(int id, int star = 0, string content = "")
        {
            if (!Functions.IsLogin())
            {
                Functions.returnlink = $"/hostel/{id}.html";
                return RedirectToAction("Index", "Login");
            }
            var check = _context.TbReviews.FirstOrDefault(i => i.Idhostel == id && i.Iduser == Functions.userid);
            if (check != null)
            {
                check.Star = star;
                check.Review = content;
                _context.TbReviews.Update(check);
                await _context.SaveChangesAsync();
            }
            else
            {
                var review = new TbReview
                {
                    Iduser = Functions.userid,
                    Idhostel = id,
                    Star = star,
                    Review = content
                };
                _context.TbReviews.Add(review);
                await _context.SaveChangesAsync();
            }
            return Redirect($"/hostel/{id}.html");
        }
    }
}
