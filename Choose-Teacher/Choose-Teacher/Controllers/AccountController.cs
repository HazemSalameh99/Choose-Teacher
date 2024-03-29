﻿using Choose_Teacher.Data;
using Choose_Teacher.Models;
using Choose_Teacher.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
                var chechUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
                var chechTeacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Email == model.Email);
                if (chechUser != null || chechTeacher != null)
                {
                    ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                    return View(model);

                }
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
                var checkUser = await _context.Users.AnyAsync(u => u.Email == model.Email && u.Password == model.Password && u.Role == Role.User);
                var checkTeacher = await _context.Teachers.AnyAsync(t => t.Email == model.Email && t.Password == model.Password);
                var Admin = await _context.Users.AnyAsync(t => t.Email == model.Email && t.Password == model.Password &&t.Role==Role.Admin);
                if (checkUser)
                {
                    HttpContext.Session.SetString("Email",model.Email);
                    var userId=_context.Users.FirstOrDefault(u=>u.Email == model.Email);
                    HttpContext.Session.SetInt32("userId", userId.UserId);
                    HttpContext.Session.SetString("userName", userId.UserName);
                    return RedirectToAction("Index","Home");
                }else if (checkTeacher)
                {
                    var teacherId = _context.Teachers.FirstOrDefault(t => t.Email == model.Email);
                    HttpContext.Session.SetString("Email", model.Email);
                    HttpContext.Session.SetInt32("teacherId", teacherId.TeacherId);
                    HttpContext.Session.SetString("teacherName", teacherId.TeacherName);

                    return RedirectToAction("Index", "Home", new { area = "Dashboard" });
                }
                else if (Admin)
                {
                    var adminId = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Role==Role.Admin);
                    HttpContext.Session.SetInt32("adminId", adminId.UserId);
                    HttpContext.Session.SetString("adminName", adminId.UserName);
                    return RedirectToAction("Index", "Home", new { area = "Dashboard"});
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                    return View(model);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account"); 
        }
    }
}
