using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess;
using MyApp.DataAccess.Infrastructure.IRepository;
using MyApp.Models;
using System.CodeDom;

namespace Final_Project.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _unitOfWork.CategoryRepository.GetAll();
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
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.save();

            TempData["success"] = "Created Successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var model = _unitOfWork.CategoryRepository.GetT(x=>x.Id==id);
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
                _unitOfWork.CategoryRepository.update(categories);
                _unitOfWork.save();
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
            var model = _unitOfWork.CategoryRepository.GetT(x => x.Id == id);

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
            var category = _unitOfWork.CategoryRepository.GetT(x => x.Id == Id);

            if (category == null)
                return NotFound();

            _unitOfWork.CategoryRepository.Del(category);
            _unitOfWork.save();
            TempData["success"] = "Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
