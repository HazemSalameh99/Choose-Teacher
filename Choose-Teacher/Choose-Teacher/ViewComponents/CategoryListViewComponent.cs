using Choose_Teacher.Data;
using Microsoft.AspNetCore.Mvc;

namespace Choose_Teacher.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        ApplicationDbContext _context;
        public CategoryListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var Result = _context.Categories.ToList();
            return View(Result);
        }
    }
}
