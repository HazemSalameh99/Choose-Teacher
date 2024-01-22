using Choose_Teacher.Data;
using Microsoft.AspNetCore.Mvc;

namespace Choose_Teacher.ViewComponents
{
    public class TeacherViewComponent:ViewComponent
    {
        ApplicationDbContext _context;
        public TeacherViewComponent(ApplicationDbContext context)
        {
                _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var Result=_context.Teachers.Take(6).ToList();
            return View(Result);
        }

    }
}
