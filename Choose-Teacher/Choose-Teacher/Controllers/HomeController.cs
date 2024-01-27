using Choose_Teacher.Data;
using Choose_Teacher.Models;
using Choose_Teacher.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Choose_Teacher.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
