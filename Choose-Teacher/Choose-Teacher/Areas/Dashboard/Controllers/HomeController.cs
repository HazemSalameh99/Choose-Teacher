using Microsoft.AspNetCore.Mvc;

namespace Choose_Teacher.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
