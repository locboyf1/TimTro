using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using TimTro.Models;

namespace TimTro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HostelContext _context;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HostelContext context, HttpClient httpClient)
        {
            _logger = logger;
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync("https://provinces.open-api.vn/api/?depth=1");
            ViewBag.Provinces = response;
            var hostel = _context.TbHostels.Where(i => i.IsAvailable).Where(i => i.IsApproval == true).Where(i=>i.IsShow).Include(i=>i.IduserNavigation).ToList();
            ViewBag.blogs = _context.TbBlogs.Include(i=>i.IduserNavigation).Include(i=>i.TbBlogComments).Where(i => i.IsShow).OrderByDescending(i => i.Time).Take(6).ToList();
            ViewBag.users = _context.TbUsers.OrderByDescending(i => i.TbHostels.Count()).Take(6).ToList();
            return View(hostel);
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
    }
}

