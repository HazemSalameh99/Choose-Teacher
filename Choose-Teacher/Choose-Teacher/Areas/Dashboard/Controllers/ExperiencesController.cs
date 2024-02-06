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
    public class ExperiencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExperiencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Experiences
        public async Task<IActionResult> Index()
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            var applicationDbContext = _context.Experiences.Include(e => e.Teacher)
                .Where(e => e.TeacherId == teacherId);
            if (HttpContext.Session.GetString("adminName") != null)
            {
                applicationDbContext = _context.Experiences.Include(e => e.Teacher);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dashboard/Experiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.ExperienceId == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Dashboard/Experiences/Create
        public IActionResult Create()
        {
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email");
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher=_context.Teachers.SingleOrDefault(t=>t.TeacherId== teacherId);
            var model = new Experience
            {
                Teacher = teacher,
                TeacherId = teacher.TeacherId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
            };
            return View(model);
        }

        // POST: Dashboard/Experiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExperienceId,Organization,Position,StartDate,EndDate,Description,TeacherId,CreatedDate,IsActive,IsDeleted")] Experience experience)
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher=_context.Teachers.SingleOrDefault(t=>t.TeacherId== teacherId);
            if (ModelState.IsValid)
            {
                if (teacherId.HasValue)
                {
                    experience.TeacherId = teacherId.Value;
                    experience.Teacher = teacher;
                    experience.CreatedDate = DateTime.Now;
                    experience.IsActive = true;
                    experience.IsDeleted = false;
                }
                _context.Experiences.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", experience.TeacherId);
            return View(experience);
        }

        // GET: Dashboard/Experiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences.FindAsync(id);
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher= _context.Teachers.SingleOrDefault(t=>t.TeacherId == teacherId);
            experience.TeacherId = teacher.TeacherId;
            experience.Teacher= teacher;
            if (experience == null)
            {
                return NotFound();
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", experience.TeacherId);
            return View(experience);
        }

        // POST: Dashboard/Experiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExperienceId,Organization,Position,StartDate,EndDate,Description,TeacherId,CreatedDate,IsActive,IsDeleted")] Experience experience)
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            if (id != experience.ExperienceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (teacherId.HasValue)
                    {
                        experience.TeacherId = teacherId.Value;
                        experience.Teacher = teacher;

                        _context.Update(experience);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.ExperienceId))
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
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", experience.TeacherId);
            return View(experience);
        }

        // GET: Dashboard/Experiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.ExperienceId == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Dashboard/Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experiences.Any(e => e.ExperienceId == id);
        }
    }
}
