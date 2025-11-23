using FeroCourse.Data;
using FeroCourse.Data.Dtos;
using FeroCourse.Data.Entities;
using FeroCourse.ServicesInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public IActionResult Index() => View();

    // ----------------- CREATE PAGE -----------------
    [HttpGet]
    public IActionResult CourseCreate()
    {
        LoadCategoryDropdown();
        return View(new CourseVM());
    }

    [HttpPost]
    public async Task<IActionResult> CourseCreate(CourseVM vm)
    {
        // Upload Image
        if (vm.Imagefile != null)
        {
            var imagepath = await _FileUploadService.UploadImageAsync(vm.Imagefile, "upload");
            if (!string.IsNullOrEmpty(imagepath))
            {
                vm.ThumbnailPath = imagepath;
            }
        }

        // Save Data
        var data = new Course
        {
            Title = vm.Title,
            Description = vm.Description,
            InstructorName = vm.InstructorName,
            Price = vm.Price,
            DiscountPrice = vm.DiscountPrice,
            ThumbnailPath = vm.ThumbnailPath,
            CategoryId = vm.CategoryId
        };

        _dbcontext.Courses.Add(data);
        _dbcontext.SaveChanges();

        // Reload dropdown before returning view
        LoadCategoryDropdown();

        return RedirectToAction("Courselist");
    }

    // ----------------- COURSE LIST -----------------
    public IActionResult Courselist()
    {
        var data = _dbcontext.Courses.Select(x => new CourseVM
        {
            CourseId = x.CourseId,
            Title = x.Title,
            Description = x.Description,
            InstructorName = x.InstructorName,
            Price = x.Price,
            DiscountPrice = x.DiscountPrice,
            ThumbnailPath = x.ThumbnailPath
        }).ToList();

        return View(data);
    }

    // ----------------- GET COURSE BY ID -----------------
    [HttpGet]
    public IActionResult GetCourse(int id)
    {
        var data = _dbcontext.Courses
            .Where(x => x.CourseId == id)
            .Select(x => new CourseVM
            {
                CourseId = x.CourseId,
                Title = x.Title,
                Description = x.Description,
                InstructorName = x.InstructorName,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
                ThumbnailPath = "/" + x.ThumbnailPath
            }).FirstOrDefault();

        if (data == null)
            return NotFound();

        return Json(data);
    }

    // ----------------- EDIT COURSE -----------------
    [HttpPost]
    public async Task<IActionResult> CourseEditAsync([FromForm] CourseVM vm)
    {
        if (vm.CourseId == 0)
            return BadRequest("Invalid Course");

        var course = _dbcontext.Courses.FirstOrDefault(x => x.CourseId == vm.CourseId);
        if (course == null)
            return NotFound("Course not found");

        // New Image Upload
        if (vm.Imagefile != null)
        {
            var imagepath = await _FileUploadService.UploadImageAsync(vm.Imagefile, "upload");
            if (!string.IsNullOrEmpty(imagepath))
            {
                vm.ThumbnailPath = imagepath;
            }
        }
        else
        {
            vm.ThumbnailPath = course.ThumbnailPath; // keep old image
        }

        // Update Values
        course.Title = vm.Title;
        course.Description = vm.Description;
        course.InstructorName = vm.InstructorName;
        course.Price = vm.Price;
        course.DiscountPrice = vm.DiscountPrice;
        course.CategoryId = vm.CategoryId;
        course.ThumbnailPath = vm.ThumbnailPath;

        _dbcontext.Courses.Update(course);
        _dbcontext.SaveChanges();

        return Json("success");
    }

    // ----------------- DELETE COURSE -----------------
    [HttpPost]
    public IActionResult CourseDelete(int id)
    {
        var course = _dbcontext.Courses.FirstOrDefault(x => x.CourseId == id);
        if (course != null)
        {
            _dbcontext.Courses.Remove(course);
            _dbcontext.SaveChanges();
            return Ok();
        }
        return BadRequest();
    }

    // ----------------- LOAD DROPDOWN -----------------
    private void LoadCategoryDropdown()
    {
        var AllCategorydata = _dbcontext.Categorys.ToList();
        ViewBag.CategoryDropDown = AllCategorydata.Select(x =>
            new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }
        ).ToList();
    }
}
