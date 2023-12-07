using AbaJohn.Models;
using AbaJohn  ;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace AbaJohn.Services.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        //  Abanob 
        //  johnn
        //  3amkalzeroo
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> usermanger; // بيكلم الداتا بيز 
        private readonly SignInManager<ApplicationUser> signinmanger; //  بيعمل الكوكيز 


        public AccountRepository(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            context = _context;
            usermanger = _userManager;
            signinmanger = _signInManager;
        }

        public List<IdentityRole> get_all_roles()
        {
            var cat = context.Roles.ToList();
            return (cat);
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
        public async Task<int> create_business_account(userViewModel newuser_account)
        {


            ApplicationUser user = new ApplicationUser();

            string ImgFileName = "";

            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BussAccImg/Adminimg");




                user.UserName = newuser_account.user_name;
                user.Name = newuser_account.name;
                user.Email = newuser_account.email;

                user.age = newuser_account.age;
                user.Gender = newuser_account.gender;
                user.PhoneNumber = newuser_account.phone_number;



                IdentityResult result = await usermanger.CreateAsync(user, newuser_account.password);

                if (result.Succeeded)
                {

                    await usermanger.AddToRoleAsync(user, newuser_account.Role);
                    await signinmanger.SignInAsync(user, false);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            return 0;

        }

      
    }
}