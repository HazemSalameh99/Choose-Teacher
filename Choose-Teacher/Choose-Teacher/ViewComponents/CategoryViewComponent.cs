using Choose_Teacher.Data;
using Microsoft.AspNetCore.Mvc;
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
            var Result=_context.Categories.ToList();
            var data = _context.Teachers.Count(t => t.TeacherId == t.CategoryId);
            ViewBag.Number = data;
            return View(Result);
        }
    }
}
