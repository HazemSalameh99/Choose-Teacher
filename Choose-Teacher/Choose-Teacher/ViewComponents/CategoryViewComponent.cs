using Choose_Teacher.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Choose_Teacher.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {
        ApplicationDbContext _context;
        public CategoryViewComponent(ApplicationDbContext context)
        {
                _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var Result = _context.Categories.Include(c=>c.Teachers)
                .ToList();
            return View(Result);
        }
    }
}
