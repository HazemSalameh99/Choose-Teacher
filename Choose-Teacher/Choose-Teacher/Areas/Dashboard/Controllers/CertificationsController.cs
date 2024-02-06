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
    public class CertificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Certifications
        public async Task<IActionResult> Index()
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);

            var applicationDbContext = _context.Certifications.Include(c => c.Teacher)
                .Where(c => c.TeacherId == teacherId);
            if (HttpContext.Session.GetString("adminName") != null)
            {
                applicationDbContext = _context.Certifications.Include(c => c.Teacher);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dashboard/Certifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.CertificationId == id);
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // GET: Dashboard/Certifications/Create
        public IActionResult Create()
        {
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email");
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            var model = new Certification
            {
                TeacherId = teacher.TeacherId,
                Teacher = teacher,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
            };
            return View(model);
        }

        // POST: Dashboard/Certifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Certification certification)
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            if (ModelState.IsValid)
            {
                if (teacherId.HasValue)
                {
                    certification.TeacherId = teacherId.Value;
                    certification.Teacher = teacher;
                    certification.CreatedDate = DateTime.Now;
                    certification.IsDeleted = false;
                    certification.IsActive = true;
                }
                _context.Certifications.Add(certification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", certification.TeacherId);
            return View(certification);
        }

        // GET: Dashboard/Certifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications.FindAsync(id);
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            certification.Teacher = teacher;
            certification.TeacherId = teacher.TeacherId;
            if (certification == null)
            {
                return NotFound();
            }
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", certification.TeacherId);
            return View(certification);
        }

        // POST: Dashboard/Certifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CertificationId,CertificationName,IssuingAuthority,IssueDate,TeacherId,CreatedDate,IsActive,IsDeleted")] Certification certification)
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var teacher = _context.Teachers.SingleOrDefault(t => t.TeacherId == teacherId);
            if (id != certification.CertificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (teacherId.HasValue)
                    {
                        certification.TeacherId = teacherId.Value;
                        certification.Teacher = teacher;


                        _context.Certifications.Update(certification);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationExists(certification.CertificationId))
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
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Email", certification.TeacherId);
            return View(certification);
        }

        // GET: Dashboard/Certifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.CertificationId == id);
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // POST: Dashboard/Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certification = await _context.Certifications.FindAsync(id);
            if (certification != null)
            {
                _context.Certifications.Remove(certification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificationExists(int id)
        {
            return _context.Certifications.Any(e => e.CertificationId == id);
        }
    }
}
