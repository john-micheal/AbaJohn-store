using AbaJohn.Models;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AbaJohn.Services.user;

using AbaJohn;

namespace AbaJohn.Controllers
{
    [Authorize(Roles = "seller")]
    public class SellerController : Controller
    {


        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;
        private readonly Iuser userRepository;

        public SellerController(IProductRepository _productRepository, IcategoeryRepository _categoeryRepository
            , Iuser _UserRepository)
        {

            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
            userRepository = _UserRepository;
        }
        [Authorize(Roles = "admin , seller")]
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult home()
        {

            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult SellerProfile()
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
        public IActionResult ChangeSellerInfo()
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
        public IActionResult ChangeSellerInfo( UserWithaddressViewModel user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var userInfo = userRepository.UpdateUserInfo( user, "Seller");
                    return RedirectToAction("SellerProfile", "seller");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View("errorpage");
                }
            }
            return View(user);
        }
    }
}
