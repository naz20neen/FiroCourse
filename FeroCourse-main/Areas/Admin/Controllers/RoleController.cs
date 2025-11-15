using System.Data;
using FeroCourse.Areas.Admin.ViewModels;
using FeroCourse.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace FeroCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;


        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string RoleName)
        {

            if (!String.IsNullOrEmpty(RoleName))
            {
                bool isHave = await _roleManager.RoleExistsAsync(RoleName);
                if (!isHave)
                {
                    await _roleManager.CreateAsync(new IdentityRole(RoleName));
                    TempData["Message"] = "Success";
                    TempData["Type"] = "alert-success";
                    return View();
                }
            }
            TempData["Message"] = "Error";
            TempData["Type"] = "alert-danger";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Assign()
        {
            var AllUserData = _userManager.Users.ToList();
            var AllRoles = _roleManager.Roles.ToList();

            var UserDropDown = AllUserData.Select(x => 
                    new SelectListItem {Value = x.Id,Text=x.Email }
             
            ).ToList();

            var RolesDropDown = AllRoles.Select(x =>
                   new SelectListItem { Value = x.Name, Text = x.NormalizedName }

           ).ToList();


            ViewBag.UserDropDown = UserDropDown;

            ViewBag.RolesDropDown = RolesDropDown;

            var userRoles = new List<UserRoleVM>();

            foreach (var user in AllUserData) {
                var CurrUserRoles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new UserRoleVM
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = string.Join(",", CurrUserRoles)

                });


            }
            return View(userRoles);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssignSubmit(string userId, string RoleName)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || !await _roleManager.RoleExistsAsync(RoleName))
            {
                return View("Error");
            }

         

            var createRole = await _userManager.AddToRoleAsync(user, RoleName);

            if (createRole.Succeeded)
           

            return RedirectToAction("Assign");
            return View("Error");


        }

    }
}
