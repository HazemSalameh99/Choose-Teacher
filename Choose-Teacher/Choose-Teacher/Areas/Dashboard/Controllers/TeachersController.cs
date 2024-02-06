using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Choose_Teacher.Data;
using Choose_Teacher.Models;
using Choose_Teacher.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Choose_Teacher.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public TeachersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Dashboard/Teachers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teachers.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dashboard/Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Dashboard/Teachers/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Dashboard/Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = new Teacher
                {
                    TeacherName = model.TeacherName,
                    Email = model.Email,
                    Password = model.Password,
                    phoneNumber=model.phoneNumber,
                    Category=model.Category,
                    CategoryId = model.CategoryId,
                    City = City.Amman,
                    Loacation = "Amman",
                    IsDeleted = false,
                    IsActive = true,
                    CreatedDate = DateTime.Now,

                };
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }

        // GET: Dashboard/Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = await _context.Teachers.FindAsync(teacherId);
            var teacherViewModel = new EditTeacherViewModel
            {
                TeacherName = teacher.TeacherName,
                Email = teacher.Email,
                phoneNumber = teacher.phoneNumber,
                CategoryId = teacher.CategoryId,
                City = teacher.City,
                Bio = teacher.Bio,
                Category = teacher.Category,
                Loacation = teacher.Loacation,
                PriceOfHour = teacher.PriceOfHour
            };
            var teacerId = teacher.TeacherId;
            var teacherImg = teacher.ImageUrl;
            TempData["teacerId"] = teacerId;
            TempData["teacherImg"] = teacherImg;
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", teacher.CategoryId);
            return View(teacherViewModel);
        }

        // POST: Dashboard/Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTeacherViewModel model)
        {
            //if (id != teacher.TeacherId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                string fileName = UploadNewFile(model);
                var teacher = await _context.Teachers.FindAsync(id);
                teacher.TeacherName = model.TeacherName;
                teacher.Email = model.Email;
                teacher.phoneNumber = model.phoneNumber;
                teacher.City = model.City;
                teacher.Bio= model.Bio;
                teacher.PriceOfHour = model.PriceOfHour;
                teacher.CategoryId= model.CategoryId;
                teacher.Category = model.Category;
                if (model.ImageUrl != null)
                {
                    if (teacher.ImageUrl != null)
                    {
                        string rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", teacher.ImageUrl);
                        System.IO.File.Delete(rootPath);
                    }
                    teacher.ImageUrl = fileName;
                }
                try
                {
                    _context.Teachers.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }

        // GET: Dashboard/Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Dashboard/Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public string UploadNewFile(EditTeacherViewModel model)
        {
            string newFullImgName = null;
            if (model.ImageUrl != null)
            {
                string fileRoot = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                //asld544-asdcd755-ss544-_123.JPJ 
                newFullImgName = Guid.NewGuid().ToString() + "_" + model.ImageUrl.FileName;//,anx1445+_dog
                string fullPath = Path.Combine(fileRoot, newFullImgName);
                using (var myNewFile = new FileStream(fullPath, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(myNewFile);
                }
            }
            return newFullImgName;
        }
        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }
    }
}
