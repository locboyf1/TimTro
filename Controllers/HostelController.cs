using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimTro.Models;

namespace TimTro.Controllers
{
    public class HostelController : Controller
    {
        private readonly HostelContext _context;
        int pageSize = 10; // Số lượng item trên mỗi trang
        public HostelController(HostelContext context)
        {

            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var item = _context.TbHostels.Where(i => i.IsApproval).Where(i => i.IsShow).OrderByDescending(i => i.UploadDate).Include(i => i.TbImageHostels).Include(i => i.IduserNavigation).ToList();

            ViewBag.page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)item.ToList().Count / pageSize);
            return await Task.FromResult<IActionResult>(View(item));
        }

        [HttpPost]
        public async Task<IActionResult> Index(string priceRange, string areaRange, string province, string district, string ward, int IsPrivate, int IsHasParkingLot, int page = 1)
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

            var item = _context.TbHostels.Where(i => i.IsShow).Where(i => i.Price >= minPrice && i.Price <= maxPrice).Where(i => i.Area >= minArea && i.Area <= maxArea).Where(i => i.IsApproval).OrderByDescending(i => i.UploadDate).Include(i => i.TbImageHostels).Include(i => i.IduserNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(province))
                item = item.Where(h => h.Province == province);

            if (!string.IsNullOrEmpty(district))
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

            var hostel = item.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)item.ToList().Count / pageSize);

            return await Task.FromResult<IActionResult>(View(hostel));
        }
    }
}
