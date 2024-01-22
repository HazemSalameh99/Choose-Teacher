using Choose_Teacher.Data;
using Choose_Teacher.Models;
using Choose_Teacher.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Choose_Teacher.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
                _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City,
                    Password = model.Password,
                    Role = Role.User,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                };
                var chechUser=_context.Users.Where(u=>u.Email==model.Email);
                if (chechUser!=null)
                {
                    ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                    return View(model);

                }
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkUser =await _context.Users.Where(u => u.Email == model.Email && u.Password == model.Password).ToListAsync();
                if (checkUser.Any())
                {
                    _context.Users.Include(u => u.UserId);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
