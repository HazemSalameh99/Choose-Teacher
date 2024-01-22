using Choose_Teacher.Data;
using Choose_Teacher.Models;
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

        public async Task<IActionResult> Teachers(int? id)
        {
            if (id == null) { return NotFound(); }
            var Data = _context.Categories.Find(id);
            ViewData["CategoryName"] = Data.CategoryName;
            var Result=await _context.Teachers.Where(t=>t.CategoryId == id).OrderByDescending(t=>t.CreatedDate).ToListAsync();
            if (Result==null) { return NotFound(); }
            return View(Result);
        }
        public async Task<IActionResult> AllTeachers()
        {
            var Result = await _context.Teachers.OrderBy(t=>t.CreatedDate).ToListAsync();
            if (Result == null) { return NotFound(); }
            return View(Result);
        }
        public async Task<IActionResult> TeacherDetails(int? id)
        {
            if (id == null) { return NotFound(); }
            var Data = await _context.Teachers.FindAsync(id);
            if (Data == null) { return NotFound(); }
            return View(Data);
        } 
        public IActionResult Contact()
        {
            return View();
        }
    }
}
