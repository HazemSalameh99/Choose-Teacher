using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Choose_Teacher.Data;
using Choose_Teacher.Models;
using Microsoft.Extensions.Hosting;
using Choose_Teacher.Models.ViewModels;

namespace Choose_Teacher.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Dashboard/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Dashboard/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Dashboard/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = UploadNewFile(model);
                Category category = new Category
                {
                    CategoryName = model.CategoryName,
                    CategoryDesc = model.CategoryDesc,
                    CategoryImg = fileName,

                };
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Dashboard/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            var categoryViewModel = new CategoryViewModel()
            {
                CategoryName = category.CategoryName,
                CategoryDesc = category.CategoryDesc,
                
            };
            var categoryId=category.CategoryId;
            var categoryImg = category.CategoryImg;
            TempData["categoryImg"]=categoryImg;
            TempData["categoryId"]=categoryId;
            if (category == null)
            {
                return NotFound();
            }
            return View(categoryViewModel);
        }

        // POST: Dashboard/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CategoryViewModel model)
        {
            //if (id != category.CategoryId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                string fileName=UploadNewFile(model);
                var category = await _context.Categories.FindAsync(id);
                category.CategoryName = model.CategoryName;
                category.CategoryDesc = model.CategoryDesc;
                if (model.CategoryImg!=null)
                {
                    if (category.CategoryImg!=null)
                    {
                        string rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", category.CategoryImg);
                        System.IO.File.Delete(rootPath);
                    }
                    category.CategoryImg = fileName;
                }
                
                try
                {
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(model);
        }

        // GET: Dashboard/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Dashboard/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public string UploadNewFile(CategoryViewModel model)
        {
            string newFullImgName = null;
            if (model.CategoryImg != null)
            {
                string fileRoot = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                //asld544-asdcd755-ss544-_123.JPJ 
                newFullImgName = Guid.NewGuid().ToString() + "_" + model.CategoryImg.FileName;//,anx1445+_dog
                string fullPath = Path.Combine(fileRoot, newFullImgName);
                using (var myNewFile = new FileStream(fullPath, FileMode.Create))
                {
                    model.CategoryImg.CopyTo(myNewFile);
                }
            }
            return newFullImgName;
        }
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
