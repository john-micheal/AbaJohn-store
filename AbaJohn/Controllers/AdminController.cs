using AbaJohn.Models;
using AbaJohn.Services.AccountRepository;
using AbaJohn;

using AbaJohn.Services.user;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AbaJohn.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
      
        private readonly   IcategoeryRepository categoeryRepository;
        private readonly   IProductRepository   productRepository;
        private readonly   IAccountRepository   accountRepository;
        private readonly Iuser userRepository;

        public AdminController(IcategoeryRepository _categoeryRepository,
                               IProductRepository _productRepository,
                               IAccountRepository _accountRepository,
                               Iuser _userRepository){
            categoeryRepository = _categoeryRepository;
            productRepository   = _productRepository;
            accountRepository   = _accountRepository;
            userRepository      = _userRepository;
        }
     
        public IActionResult Index()
        {
            return  View();
        }
  



        public IActionResult Delete_product(int id)
        {
            try
            {
                productRepository.Delete(id);
                return RedirectToAction("Show_all_product", "AdminServics");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult AdminProfile()
        {
            try
            {
                var username = User.Identity.Name;
                var userInfo = userRepository.GetUserInfo(username);
                return View(userInfo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("errorpage");
            }
        }

        [HttpGet]
        public IActionResult ChangeAdminInfo()
        {
            try
            {
                var username = User.Identity.Name;
                var userInfo = userRepository.GetUserInfo(username);
                return View(userInfo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("errorpage");
            }
        }
        [HttpPost]
        public IActionResult ChangeAdminInfo(UserWithaddressViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userInfo = userRepository.UpdateUserInfo(user ,"Admin");
                    return RedirectToAction("AdminProfile", "admin");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View("errorpage");
                }
            }
            return View(user);
        }

        public IActionResult Show_users ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Show_users(string id)
        {
           
                var user_list = userRepository.ShowUsers(id);
                
                return PartialView("_showuserpartil", user_list);
          
        }



    }
}
