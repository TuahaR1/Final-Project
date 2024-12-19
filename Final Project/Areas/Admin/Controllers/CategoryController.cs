using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess;
using MyApp.DataAccess.Infrastructure.IRepository;
using MyApp.Models;
using System.CodeDom;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
             categoryVM.categories= _unitOfWork.CategoryRepository.GetAll();
            return View(categoryVM);
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

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM category = new CategoryVM();
            if (id == 0 || id == null)
            {
                return View(category);
            }
            else
            {
                var model = _unitOfWork.CategoryRepository.GetT(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                else
                {
                    category.category = model;
                    return View(category);
                }
            }
         

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM vm)
        {
           
                if (vm.category.Id == 0 || vm.category.Id == null)
                {
                    _unitOfWork.CategoryRepository.Add(vm.category);
                    _unitOfWork.save();
                    TempData["success"] = "Updated Successfully!";
                }
                else
                {
                    _unitOfWork.CategoryRepository.update(vm.category);
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

        [HttpPost, ActionName("Delete")]
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
