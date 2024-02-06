using Choose_Teacher.Data;
using Choose_Teacher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Choose_Teacher.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class BookingsController : Controller
    {
        ApplicationDbContext _context;
        public BookingsController(ApplicationDbContext context)
        {
                _context = context;
        }
        public IActionResult Bookings()
        {
            var teacherId = HttpContext.Session.GetInt32("teacherId");
            var adminId = HttpContext.Session.GetInt32("adminId");
            if (teacherId.HasValue)
            {
                var bookingTeacher =_context.Bookings
                .Include(b => b.User)
                .Include(b=>b.Teacher)
                .Where(b => b.Status != Status.Canceled)
                .Where(b => b.TeacherId == teacherId);
                return View(bookingTeacher);
            }else if (adminId.HasValue) {
                var bookingTeacher = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Teacher)
                .Where(b => b.Status != Status.Canceled);
                return View(bookingTeacher);
            }
            return RedirectToAction("Login", "Account",new {area=""});
        }

        [HttpPost]
        public IActionResult ApproveBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking != null)
            {
                booking.Status = Status.Approved;
                _context.SaveChanges();
            }

            // Redirect to teacher's dashboard or any other appropriate page
            return RedirectToAction("Bookings");
        }

        [HttpPost]
        public IActionResult RejectBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking != null)
            {
                booking.Status = Status.Rejected;
                _context.SaveChanges();
            }

            // Redirect to teacher's dashboard or any other appropriate page
            return RedirectToAction("Bookings");
        }
    }
}
