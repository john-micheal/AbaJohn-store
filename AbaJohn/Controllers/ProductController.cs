using AbaJohn.Models;
using AbaJohn;

using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AbaJohn.Services.Itemss;
using NuGet.Protocol;
using AbaJohn.Services.user;

namespace AbaJohn.Controllers
{
    //Authorize(Roles = "admin")]
    public class ProductController : Controller
    {

        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;
        private readonly IItem itemRepository;

        public ProductController(IProductRepository _productRepository, IcategoeryRepository _categoeryRepository, IItem _item)
        {

            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
            itemRepository = _item;
        }
        public IActionResult ProductDetails(int Id)
        {
            var product = productRepository.get_product_byid(Id);
            return View(product);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Show_all_product()
        {

            List<Product> products = productRepository.get_all_product();
            return View(products);
        }
        [Authorize(Roles = "admin , seller")]
        public IActionResult ShowProductSeller()
        {
            ViewBag.massege = TempData["massege"];
            var username = User.Identity?.Name;
            List<Product> products = productRepository.GetSellerProducts(username);
            return View("Show_all_product", products);
        }
      
        public IActionResult ShowProductsByGender(string ProductGender, int PageNo = 1)
        {
            if (ProductGender == null || ProductGender == "")
                RedirectToAction("index", "home");

            var productList = productRepository.GetProductsByGender(ProductGender);

            var model = new ProductListVM_Paging
            {
                products = productList,
                CurrentPage = PageNo,
                NoOfRecordPerPage = 9,
                ProductGender = ProductGender
            };
            return View(model);
        }

        // remmber to but parmeter in class isted of  multi parmeter 
        public IActionResult ShowProductsFilter(string ProductGender, string Category , double? MinPrice, double? MaxPrice,
         string Color,string size ,   int PageNo = 1)
        {
            var productList = productRepository.ProductsFilter(ProductGender, Category, MinPrice, MaxPrice, Color, size);

            var model = new ProductListVM_Paging
            {
                products = productList,
                CurrentPage = PageNo,
                NoOfRecordPerPage = 9,
                ProductGender = ProductGender
            };
           return PartialView("_ProductsAndPagingpartial", model);
        }
     
        [Authorize(Roles = "admin , seller")]
        public IActionResult Add_product()
        {
               
            var username = User.Identity?.Name;
            ViewBag.id = productRepository.getseller_id(username);

            ViewBag.cat = categoeryRepository.get_all();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin , seller")]
        public IActionResult Add_product(productViewModel new_product)
        {
            ViewBag.cat = categoeryRepository.get_all();
            if (ModelState.IsValid)
            {

                productRepository.create(new_product);

                return RedirectToAction("ShowProductSeller", "Product");

            }
            else
            {
                ModelState.AddModelError("", "Error!");
                return View(new_product);
            }


        }

        [Authorize(Roles = "admin , seller")]
        public IActionResult Edit_product(int id)
        {
            var username = User.Identity?.Name;
            ViewBag.id = productRepository.getseller_id(username);
            //ViewData["old_product"] = productRepository.get_product_byid(id);
            ViewBag.cat = categoeryRepository.get_all();
            var product = productRepository.get_product_byid(id);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin , seller")]
        public IActionResult Edit_product([FromRoute] int id, productViewModel product)
        {
            if (ModelState.IsValid)
            {
                productRepository.update(id, product);

                return RedirectToAction("ShowProductSeller", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(product);
        }
        [Authorize(Roles = "admin , seller")]
        public IActionResult Delete_product(int id)
        {
            try
            {
                productRepository.Delete(id);
                return RedirectToAction("ShowProductSeller", "product");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);

                return RedirectToAction("Index", "Home");
            }
        }




    }
}