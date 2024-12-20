using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess;
using MyApp.DataAccess.Infrastructure.IRepository;
using MyApp.Models;
using System.CodeDom;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;


        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            ProductVM productVM = new ProductVM();
            productVM.products= _unitOfWork.ProductRepository.GetAll();
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Product product)
        {
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.save();

            TempData["success"] = "Created Successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM product = new ProductVM();
            if (id == 0 || id == null)
            {
                return View(product);
            }
            else
            {
                var model = _unitOfWork.ProductRepository.GetT(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                else
                {
                    product.product = model;
                    return View(product);
                }
            }
         

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM productVM)
        {
           
                if (productVM.product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(productVM.product);
                    _unitOfWork.save();
                    TempData["success"] = "Updated Successfully!";
                }
                else
                {
                    _unitOfWork.ProductRepository.update(productVM.product);
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
            var model = _unitOfWork.ProductRepository.GetT(x => x.Id == id);

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
            var product = _unitOfWork.ProductRepository.GetT(x => x.Id == Id);

            if (product == null)
                return NotFound();

            _unitOfWork.ProductRepository.Del(product);
            _unitOfWork.save();
            TempData["success"] = "Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
