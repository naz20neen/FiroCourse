using FeroCourse.Data;
using FeroCourse.Data.Dtos;
using FeroCourse.Data.Entities;
using FeroCourse.ServicesInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeroCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseClassController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public CourseClassController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // ============================
        // GET: Create Course Class
        // ============================
        [HttpGet]
        public IActionResult CourseClassCreate(int id)
        {
            
            LoadClassDropdown();

            var course = _dbcontext.Courses.FirstOrDefault(x => x.CourseId == id);

            ViewBag.CourseTitle = course?.Title;

            var vm = new CourseClassVM { CourseId = id };

            return View(vm);
        }

        // ============================
        // POST: Create Course Class
        // ============================
        [HttpPost]
        public async Task<IActionResult> CourseClassCreate(CourseClassVM viewmodel)
        {
            // ----- Order No validation -----
            if (viewmodel.OrderNo == null || viewmodel.OrderNo <= 0)
            {
                ModelState.AddModelError("OrderNo", "Order No must be greater than 0.");
            }
            if (string.IsNullOrEmpty(viewmodel.VideoUrl))
            {
                ModelState.AddModelError("VideoUrl", "Video URL is required.");
            }
            else if (!viewmodel.VideoUrl.StartsWith("http://") && !viewmodel.VideoUrl.StartsWith("https://"))
            {
                ModelState.AddModelError("VideoUrl", "Video URL must start with http:// or https://");
            }

            // ----- Document URL validation -----
            if (string.IsNullOrEmpty(viewmodel.DocumentUrl))
            {
                ModelState.AddModelError("DocumentUrl", "Document URL is required.");
            }
            else if (!viewmodel.DocumentUrl.StartsWith("http://") && !viewmodel.DocumentUrl.StartsWith("https://"))
            {
                ModelState.AddModelError("DocumentUrl", "Document URL must start with http:// or https://");
            }

           
        var data = new CourseClass
            {
                CourseId = viewmodel.CourseId,
                ClassTitle = viewmodel.Title,
                Description = viewmodel.Description,
                VideoUrl = viewmodel.VideoUrl,
                DocumentUrl = viewmodel.DocumentUrl,
                OrderNo = viewmodel.OrderNo,
                Duration = viewmodel.Duration,
                IsFreePreview = viewmodel.IsFreePreview,
                IsPublished = viewmodel.IsPublished,
                CreatedAt = DateTime.UtcNow
            };

            _dbcontext.Classes.Add(data);
            await _dbcontext.SaveChangesAsync();

            // Redirect back to the class list for this course
            return RedirectToAction("CourseClassCreate", new { id = viewmodel.CourseId });
        }

        // ============================
        // Load Dropdown Items
        // ============================
        private void LoadClassDropdown()
        {
            ViewBag.Courses = _dbcontext.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.Title
                })
                .ToList();
        }
    }
}
