using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess;
using MyApp.Models;
using System.CodeDom;

namespace Final_Project.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;


        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["success"] = "Created Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var model = _context.Categories.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category categories)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(categories);
                _context.SaveChanges();
                TempData["success"] = "Updated Successfully!";

            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var model = _context.Categories.Find(id);
           
            if (model == null)
            {
                return NotFound();
            }
            return View(model);

        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            var category = _context.Categories.Find(Id);

            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
