using Choose_Teacher.Data;
using Microsoft.AspNetCore.Mvc;

namespace Choose_Teacher.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        ApplicationDbContext _context;
        public SliderViewComponent(ApplicationDbContext context)
        {
                _context = context; 
        }
        public IViewComponentResult Invoke()
        {
            var Result=_context.Sliders.ToList();
            return View(Result);
        }
    }
}
