using FeroCourse.Data;
using FeroCourse.Data.Dtos;
using FeroCourse.Data.Entities;
using FeroCourse.Data.ViewModels;
using FeroCourse.Services;
using FeroCourse.ServicesInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FeroCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IFileUploadService _FileUploadService;

        public CourseController(ApplicationDbContext dbcontext, IFileUploadService FileUploadService)
        {
            _dbcontext = dbcontext;
            _FileUploadService = FileUploadService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CourseCreate()
        {
            var AllCategorydata = _dbcontext.Categorys.ToList();

            var CategoryDropDown = AllCategorydata.Select(static x =>
                    new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName }

            ).ToList();

            ViewBag.CategoryDropDown = CategoryDropDown;

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CourseCreate(CourseVM vm)
        {

            if (vm.Imagefile != null)
            {
                var imagepath = await _FileUploadService.UploadImageAsync(vm.Imagefile, "upload");

                if (!string.IsNullOrEmpty(imagepath))
                {

                    vm.ThumbnailPath = imagepath;
                }

            }
            var data = new Course();

            data.Title = vm.Title;

            data.Description = vm.Description;
            data.InstructorName = vm.InstructorName;
            data.Price = vm.Price;
            data.DiscountPrice = vm.DiscountPrice;
            data.ThumbnailPath = vm.ThumbnailPath;

            _dbcontext.Courses.Add(data);
            _dbcontext.SaveChanges();
            return View(vm);
        }

        
        public IActionResult Courselist()
        {

            var data = _dbcontext.Courses.Select(x => new CourseVM
            {
                Title = x.Title,
                Description = x.Description,
                InstructorName = x.InstructorName,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
                ThumbnailPath = x.ThumbnailPath,
            }).ToList();

            return View(data);
        }


    }

}




