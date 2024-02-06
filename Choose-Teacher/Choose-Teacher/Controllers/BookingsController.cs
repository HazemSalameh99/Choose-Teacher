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

namespace Choose_Teacher.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Book(int? id)
        {
            if (id == null) { return NotFound(nameof(Book)); }
            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.TeacherId == id);
            if (teacher == null) { return NotFound($"{nameof(Book)} is null"); }
            var model = new Booking
            {
                TeacherId = teacher.TeacherId,
                Teacher = teacher,
                BookingDate = DateTime.Now,
                Status = Status.Pending,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Book(Booking model)
        {

            var teacher = _context.Teachers.Find(model.TeacherId);

            var userId = HttpContext.Session.GetInt32("userId");
            if (userId.HasValue)
            {

                var hour =(decimal)(model.EndTime.Hours - model.StartTime.Hours);
                model.Price = (double)(hour * teacher.PriceOfHour);
                model.TeacherId = teacher.TeacherId;
                model.UserId = userId.Value;
                _context.Bookings.Add(model);
                _context.SaveChanges();
            }
            else { return RedirectToAction("Login", "Account", new { returnUrl = "/Bookings/Book" }); }

            // Redirect to a confirmation page or take any other action
            return RedirectToAction("Bookings", "Bookings");


            // If ModelState is not valid, redisplay the form with validation errors

        }
        public IActionResult Bookings()
        {
            // Get the user ID from the session or wherever it's stored
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId.HasValue)
            {
                // Retrieve the user's bookings
                var userBookings = _context.Bookings
                    .Include(x=>x.Teacher)
                    .Where(b=>b.Status!=Status.Canceled)
                    .Where(b => b.UserId == userId.Value)
                    .ToList();
                

                return View(userBookings);
            }

            // Handle the case where the user is not authenticated
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult CancelBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking != null)
            {
                // Check if the logged-in user is the owner of the booking
                var userId = HttpContext.Session.GetInt32("userId");

                if (userId.HasValue && booking.UserId == userId.Value)
                {
                    // Implement cancellation logic (e.g., set status to canceled)
                    booking.Status = Status.Canceled;
                    _context.SaveChanges();
                    return RedirectToAction("Bookings");
                }
            }

            // Handle unauthorized access or other error scenarios
            return RedirectToAction("Error");
        }
    }
}
