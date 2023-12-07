using AbaJohn.Models;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbaJohn.Controllers
{
    //[Authorize(Roles = "admin")]
    public class RoleController : Controller
    {

        private RoleManager<IdentityRole> rolemanger;

        public RoleController(RoleManager<IdentityRole> _rolemanger)
        {
            rolemanger = _rolemanger;
        }


        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel newrole)
        {
            if (ModelState.IsValid==true) 
            {
             IdentityRole role = new IdentityRole();
                role.Name = newrole.RoleName;

                IdentityResult result = await rolemanger.CreateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("AddBussnessACount", "account");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                
            }
            return View(newrole);
        }


    }
}