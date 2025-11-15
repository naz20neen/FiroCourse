using System.Threading.Tasks;
using System.Xml.Linq;
using FeroCourse.Data;
using FeroCourse.Data.Dtos;
using FeroCourse.Data.Entities;
using FeroCourse.Services;
using FeroCourse.ServicesInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace FeroCourse.Areas.User.Controllers
{

    [Area("User")]
    [Authorize(Roles = StaticMessages.User)]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileUploadService _fileuploadservice;
        public ProfileController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IFileUploadService fileuploadservice)
        {
            _context = context;
            _userManager = userManager;
            _fileuploadservice = fileuploadservice;
        }



        public async Task<IActionResult> Index(UserProfileVM vmdata)
        { 

            var userdata = await _userManager.GetUserAsync(User);
            if (userdata == null)
                return NotFound();

            vmdata.Name = userdata.Name ?? "";
            vmdata.PhoneNumber = userdata.PhoneNumber ?? "";
            vmdata.Email = userdata.Email ?? "";
            vmdata.ProfilePicturePath = userdata.ProfilePicturePath ?? "";



            return View(vmdata);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserProfileVM vmdata)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
            return NotFound();

            if (vmdata.ProfileImage != null && vmdata.ProfileImage.Length > 0) 
            {
                user.ProfilePicturePath = await _fileuploadservice.UploadImageAsync(vmdata.ProfileImage,Common.ImageUpload);
            }

            user.Name = vmdata.Name;
            user.PhoneNumber = vmdata.PhoneNumber;
           

            var result= await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "Profile Update Succesful!";
                TempData["Icon"] = "alert-success";
                return RedirectToAction("Index");
            }


            TempData["Message"] = "Profile Update Failed!";
            TempData["Icon"] = "alert-danger";
            return RedirectToAction("Index", vmdata);
        }

       
    }
}
