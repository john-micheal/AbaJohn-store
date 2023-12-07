using AbaJohn.Models;
using AbaJohn.Services.AccountRepository;
using AbaJohn.ViewModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbaJohn.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanger; // بيكلم الداتا بيز 
        private readonly SignInManager<ApplicationUser> signinmanger; //  بيعمل الكوكيز 
        private readonly IAccountRepository accountRepository;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, IAccountRepository _accountRepository)
        {
            usermanger = _userManager;
            signinmanger = _signInManager;
            accountRepository = _accountRepository;
        }

        [Authorize]
        public IActionResult index()
        {
            return View();
        }

        public IActionResult registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> registration(registrationuserViewModel newuser_account)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = newuser_account.user_name;
                user.Name = newuser_account.name;
                user.Email = newuser_account.email;
              //  user.img = newuser_account.image;
                user.age = newuser_account.age;
                user.Gender = newuser_account.gender;
                user.PhoneNumber = newuser_account.phone_number;

                IdentityResult result = await usermanger.CreateAsync(user, newuser_account.password);

                if (result.Succeeded == true)
                {
                    await signinmanger.SignInAsync(user, false);
                    return RedirectToAction("index","home");

                }
                else
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                       
                    }
                return View(newuser_account);
            }
            return View(newuser_account);

        }
      
        public IActionResult login(string? returnurl = "~/Home/Index")
        {
            ViewData["returnurl"] = returnurl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> login(loginViewModel loginaccount, string? returnurl ="~/Home/Index")
        {
            if(ModelState.IsValid==true)
            {
                var user = await usermanger.FindByNameAsync(loginaccount.UserName);


                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signinmanger.PasswordSignInAsync(user, loginaccount.password, loginaccount.rememberme, false);
                    if (result.Succeeded == true)
                    {
                        var userRole = await usermanger.GetRolesAsync(user);
                        if (userRole.Contains("seller"))
                        {
                            return LocalRedirect("~/seller/index");
                        }
                        else if (userRole.Contains("admin"))
                        {
                            return LocalRedirect("~/admin/index");
                        }

                        return LocalRedirect(returnurl);
                    }
                    else
                    {
                        ModelState.AddModelError("","Password Is Not Correct");
                        return View(loginaccount);
                    }
                }else
                {
                    ModelState.AddModelError("", " User Name is Not Correct !");
                    return View(loginaccount);
                }
            }
            return View(loginaccount);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AddBussnessACount()
        {
            ViewBag.roles = accountRepository.get_all_roles();
            return View();
        }

        public string GenerateUniqueImageName()
        {
            // Get the current date and time
            DateTime now = DateTime.Now;

            // Generate a unique name using a combination of timestamp and random number
            string uniqueName = $"{now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";

            // Return the unique name
            return uniqueName;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddBussnessACount(userViewModel newuser_account)
        {
            ViewBag.roles = accountRepository.get_all_roles();
            ApplicationUser user = new ApplicationUser();
            Address user_address = new Address();
            ApplicationDbContext context = new ApplicationDbContext();

            string ImgFileName = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string ProfileImage = "";
                    string path = "";

                    if (newuser_account.Role == "admin")
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/BussAccImg/Adminimg");
                    }
                    else if (newuser_account.Role == "seller")
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/BussAccImg/SellerImg");
                    }

                    // Create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    if (newuser_account.ImageFile != null)
                    {
                        // Generate a unique file name for BaseImg
                        ImgFileName = GenerateUniqueImageName() + Path.GetExtension(newuser_account.ImageFile.FileName);
                        string baseImgFilePath = Path.Combine(path, ImgFileName);

                        using (var stream = new FileStream(baseImgFilePath, FileMode.Create))
                        {
                            newuser_account.ImageFile.CopyTo(stream);
                        }
                        user.img = ImgFileName;
                    }
                   
                    user.UserName = newuser_account.user_name;
                    user.Name = newuser_account.name;
                    user.Email = newuser_account.email;

                    user.age = newuser_account.age;
                    user.Gender = newuser_account.gender;
                    user.PhoneNumber = newuser_account.phone_number;
                 
                    user_address.home_number = newuser_account.home_number;
                    user_address.country = newuser_account.country;
                    user_address.city = newuser_account.city;
                    user_address.street_name = newuser_account.street_name;


                    var user_id = user.Id;
                    user_address.User_id = user_id;

                  

                    IdentityResult result = await usermanger.CreateAsync(user, newuser_account.password);

                    if (result.Succeeded)
                    {
                        //  role changed here 
                        await usermanger.AddToRoleAsync(user, newuser_account.Role);
                     //  await signinmanger.SignInAsync(user, false);
                        await context.Addresses.AddAsync(user_address);
                        await context.SaveChangesAsync();

                        return RedirectToAction("index", "home");
                    }
                    else
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);

                        }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);

                }
            }
            return View(newuser_account);


        }
        public async Task<IActionResult> logout()
        {
            await signinmanger.SignOutAsync();
            return RedirectToAction("login", "Account");
        }


    }
}
