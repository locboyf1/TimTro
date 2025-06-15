using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimTro.Models;

namespace TimTro.ViewComponents

{
    public class MenuMobileViewComponent : ViewComponent
    {
        private readonly HostelContext _context;

        public MenuMobileViewComponent(HostelContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbMenus.Where(m=>m.IsShow == true).OrderBy(m=>m.Position).ToList();
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
