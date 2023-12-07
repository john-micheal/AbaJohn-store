using AbaJohn.Models;
using AbaJohn  ;

using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AbaJohn.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {



        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;

        public CategoryController(IProductRepository _productRepository, IcategoeryRepository _categoeryRepository)
        {

            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
        }
        [HttpGet]
        public IActionResult Show_all_category()
        {
            var categories = categoeryRepository.get_all();


            return View(categories);
        }
        public IActionResult Add_categoery()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_categoery(categoeryViewModel new_category_view)
        {
            if (ModelState.IsValid)
            {

                categoeryRepository.create(new_category_view);
                return RedirectToAction("Show_all_category", "Category");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(new_category_view);

        }

        public IActionResult Edit(int id)
        {
            Category old_category = categoeryRepository.get_category_id(id);
            return View(old_category);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Category old_category_view)
        {
            if (ModelState.IsValid)
            {

                categoeryRepository.update(id, old_category_view);
                return RedirectToAction("Show_all_category", "Category");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(old_category_view);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                categoeryRepository.Delete(id);
                return RedirectToAction("Show_all_category", "Category");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);//

                return RedirectToAction("Index", "Home");
            }
        }


    }
}