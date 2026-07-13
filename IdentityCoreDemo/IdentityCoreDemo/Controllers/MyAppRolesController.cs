using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCoreDemo.Controllers
{
    public class MyAppRolesController : Controller
    {
        public UserManager<IdentityUser> UserManager;
        public RoleManager<IdentityRole> RoleManager;


        public MyAppRolesController(RoleManager<IdentityRole> _RoleManager,
          UserManager<IdentityUser> _userManager)
        {
            UserManager = _userManager;
            RoleManager = _RoleManager;

        }


        public async Task<IActionResult> Index()
        {
            IdentityRole Adminrole = new IdentityRole { Name = "Admin" };
            IdentityRole Guestrole = new IdentityRole { Name = "Guest" };

            IdentityResult result = await RoleManager.CreateAsync(Adminrole);
            IdentityResult result2 = await RoleManager.CreateAsync(Guestrole);

            IdentityUser user1 = await UserManager.FindByIdAsync("e43d91f9-5a0c-46af-9472-efc30f1eff48");
            result = await UserManager.AddToRoleAsync(user1, "Admin");

            IdentityUser user2 = await UserManager.FindByIdAsync("6d0f6ed5-5dc0-4de5-b9d0-64871ed180a0");
            result = await UserManager.AddToRoleAsync(user2, "Guest");


            if (result.Succeeded)
            {
                return RedirectToPage("/");
            }
            return View();
        }
    }
}
