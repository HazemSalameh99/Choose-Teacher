using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Choose_Teacher.Data;
using Choose_Teacher.Models;

namespace Choose_Teacher.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Teachers(int? id)
        {
            if (id == null) { return NotFound(); }
            var category = _context.Categories.Find(id);
            ViewData["CategoryName"] = category.CategoryName;
            var teachers = await _context.Teachers.Where(t => t.CategoryId == id).OrderByDescending(t => t.CreatedDate).ToListAsync();
            if (teachers == null) { return NotFound(); }
            return View(teachers);
        }
        public async Task<IActionResult> AllTeachers()
        {
            var Result = await _context.Teachers.OrderBy(t => t.CreatedDate).ToListAsync();
            if (Result == null) { return NotFound(); }
            return View(Result);
        }
        public async Task<IActionResult> TeacherDetails(int? id)
        {
            if (id == null) { return NotFound(); }
            var userId = HttpContext.Session.GetInt32("userId");
            var Data = await _context.Teachers.Include(t => t.TeacherReviews).
                Include(t => t.TeacherAvailabilities).
                Include(t => t.Certifications).
                Include(t => t.Experiences).
                Include(t => t.Educations).
                Include(t=>t.Users).
                SingleOrDefaultAsync(t => t.TeacherId == id);
            

            if (Data == null) { return NotFound(); }
            return View(Data);
        }
        [HttpGet]
        public IActionResult WriteReview(int?id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var review=_context.Teachers.SingleOrDefault(r=>r.TeacherId==id);
            if (review == null)
            {
                return NotFound();
            }
            var Review = new TeacherReview
            {
                TeacherId=review.TeacherId,
                Date = DateTime.Now,
            };
            return View(Review);
        }
        [HttpPost]
        public IActionResult WriteReview(TeacherReview review)
        {
            var teacher = _context.Teachers.Find(review.TeacherId);
            var userId = HttpContext.Session.GetInt32("userId");
            var user=_context.Users.SingleOrDefault(u=>u.UserId==userId);
            HttpContext.Session.SetString("User_Name", user.UserName);
            var userName =HttpContext.Session.GetString("User_Name");

            if (teacher!=null && userId.HasValue)
            {
                review.TeacherId = teacher.TeacherId;
                review.UserId = userId.Value;
                review.User.UserName = userName;
                _context.TeacherReviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction("TeacherDetails", "Teachers", new { id = review.TeacherId });
            }
            else { return RedirectToAction("Login", "Account", new { returnUrl = $"/Teachers/TeacherDetails/{review.TeacherId}" }); }
            }
    }
}
