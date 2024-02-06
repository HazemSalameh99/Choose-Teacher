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
    public class TeacherAvailabilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherAvailabilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/TeacherAvailabilities
        public async Task<IActionResult> Index()
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);

            var applicationDbContext = _context.TeacherAvailabilities.Include(t => t.Teacher)
                .Where(t => t.TeacherId == teacherId);
            if (HttpContext.Session.GetString("adminName") !=null)
            {
                applicationDbContext = _context.TeacherAvailabilities.Include(t => t.Teacher);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dashboard/TeacherAvailabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherAvailability = await _context.TeacherAvailabilities
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherAvailabilityId == id);
            if (teacherAvailability == null)
            {
                return NotFound();
            }

            return View(teacherAvailability);
        }

        // GET: Dashboard/TeacherAvailabilities/Create
        public IActionResult Create()
        {
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email");
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            var model = new TeacherAvailability
            {
                Teacher = teacher,
                TeacherId = teacher.TeacherId,
            };
            return View(model);
        }

        // POST: Dashboard/TeacherAvailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherAvailabilityId,dayOfWeek,StartTime,EndTime,TeacherId")] TeacherAvailability teacherAvailability)
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            if (ModelState.IsValid)
            {
                if (teacherId.HasValue)
                {
                    teacherAvailability.TeacherId = teacherId.Value;
                    teacherAvailability.Teacher = teacher;
                }
                _context.TeacherAvailabilities.Add(teacherAvailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", teacherAvailability.TeacherId);
            return View(teacherAvailability);
        }

        // GET: Dashboard/TeacherAvailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherAvailability = await _context.TeacherAvailabilities.FindAsync(id);
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            teacherAvailability.TeacherId = teacher.TeacherId;
            teacherAvailability.Teacher = teacher;
            if (teacherAvailability == null)
            {
                return NotFound();
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", teacherAvailability.TeacherId);
            return View(teacherAvailability);
        }

        // POST: Dashboard/TeacherAvailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherAvailabilityId,dayOfWeek,StartTime,EndTime,TeacherId")] TeacherAvailability teacherAvailability)
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            if (id != teacherAvailability.TeacherAvailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (teacherId.HasValue)
                    {
                        teacherAvailability.TeacherId = teacherId.Value;
                        teacherAvailability.Teacher = teacher;


                        _context.Update(teacherAvailability);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherAvailabilityExists(teacherAvailability.TeacherAvailabilityId))
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
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", teacherAvailability.TeacherId);
            return View(teacherAvailability);
        }

        // GET: Dashboard/TeacherAvailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherAvailability = await _context.TeacherAvailabilities
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherAvailabilityId == id);
            if (teacherAvailability == null)
            {
                return NotFound();
            }

            return View(teacherAvailability);
        }

        // POST: Dashboard/TeacherAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherAvailability = await _context.TeacherAvailabilities.FindAsync(id);
            if (teacherAvailability != null)
            {
                _context.TeacherAvailabilities.Remove(teacherAvailability);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherAvailabilityExists(int id)
        {
            return _context.TeacherAvailabilities.Any(e => e.TeacherAvailabilityId == id);
        }
    }
}
