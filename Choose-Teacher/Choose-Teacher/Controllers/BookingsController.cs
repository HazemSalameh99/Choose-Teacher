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
            var model = new BookingViewModel
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                BookingDate = DateTime.Now,
                Status = Status.Pending,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Book(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = _context.Teachers.Find(model.TeacherId);

                var userId = HttpContext.Session.GetInt32("userId");
                if (userId.HasValue)
                {
                    var booking = new Booking
                    {
                        BookingDate = model.BookingDate,
                        BookingHour = model.BookingHour,
                        UserId = userId.Value,
                        TeacherId = model.TeacherId,
                        Status = Status.Pending
                    };
                    booking.Price = teacher.Price(model.BookingHour, teacher.PriceOfHour);
                    TempData["price"] = booking.Price;

                    _context.Bookings.Add(booking);
                    _context.SaveChanges();
                }
                else { return RedirectToAction("Login", "Account", new { returnUrl = "/Home/Book" }); }

                // Redirect to a confirmation page or take any other action
                return RedirectToAction("Bookings");
            }

            // If ModelState is not valid, redisplay the form with validation errors
            return View(model);
        }
        public IActionResult Bookings()
        {
            // Get the user ID from the session or wherever it's stored
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId.HasValue)
            {
                // Retrieve the user's bookings
                var userBookings = _context.Bookings
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
