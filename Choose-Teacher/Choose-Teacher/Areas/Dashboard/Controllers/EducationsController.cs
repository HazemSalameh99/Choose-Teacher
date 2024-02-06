using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Choose_Teacher.Data;
using Choose_Teacher.Models;

namespace Choose_Teacher.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class EducationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Educations
        public async Task<IActionResult> Index()
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);

            var applicationDbContext = _context.Educations.Include(e => e.Teacher)
                .Where(e => e.TeacherId == teacherId);
            if (HttpContext.Session.GetString("adminName") != null)
            {
                applicationDbContext = _context.Educations.Include(e => e.Teacher);
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dashboard/Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.EducationId == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Dashboard/Educations/Create
        public IActionResult Create()
        {
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email");
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);

            var model = new Education
            {
                TeacherId = teacher.TeacherId,
                Teacher = teacher,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false,

            };
            return View(model);
        }

        // POST: Dashboard/Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Education education)
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            if (ModelState.IsValid)
            {
                if (teacherId.HasValue)
                {
                    education.TeacherId = teacher.TeacherId;
                    education.Teacher = teacher;
                    education.IsDeleted = false;
                    education.CreatedDate = DateTime.Now;
                    education.IsActive = true;
                }
                _context.Educations.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", education.TeacherId);
            return View(education);
        }

        // GET: Dashboard/Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations.FindAsync(id);
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);

            education.TeacherId = teacher.TeacherId;
            education.Teacher = teacher;

            if (education == null)
            {
                return NotFound();
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", education.TeacherId);
            return View(education);
        }

        // POST: Dashboard/Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EducationId,Majoring,University,GraduationYear,TeacherId,CreatedDate,IsActive,IsDeleted")] Education education)
        {
            var teacher = _context.Teachers.Find(education.TeacherId);
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            if (id != education.EducationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (teacherId.HasValue)
                    {
                        education.Teacher = teacher;
                        education.TeacherId = teacherId.Value;

                        _context.Update(education);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.EducationId))
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
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", education.TeacherId);
            return View(education);
        }

        // GET: Dashboard/Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.EducationId == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Dashboard/Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education != null)
            {
                _context.Educations.Remove(education);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.EducationId == id);
        }
    }
}
