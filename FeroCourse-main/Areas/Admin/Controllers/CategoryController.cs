using FeroCourse.Data;
using FeroCourse.Data.Entities;
using FeroCourse.Data.ViewModels;
using FeroCourse.ServicesInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FeroCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public CategoryController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            
        }
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            var datalist = _dbcontext.Categorys.Select(x => new CategoryVM
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,
                CategoryIsActive = x.CategoryIsActive,
                
            }).ToList();

            return View(datalist);
            
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryVM viewmodel)
        {
            var data = new Category();

            data.CategoryName = viewmodel.CategoryName;
            
            data.CategoryDescription = viewmodel.CategoryDescription;
            data.CategoryIsActive = viewmodel.CategoryIsActive;

            _dbcontext.Categorys.Add(data);
            _dbcontext.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult GetCategory(int id)
        {
            var data = _dbcontext.Categorys
                    .Where(x => x.CategoryId == id)
                    .Select(x => new CategoryVM
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        CategoryDescription = x.CategoryDescription,
                        CategoryIsActive = x.CategoryIsActive,

                    }).FirstOrDefault();

            if (data == null)
                return NotFound();

            return Json(data);

            
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(CategoryVM viewmodel)
        {

            var data = new Category();

            data.CategoryName = viewmodel.CategoryName;

            data.CategoryDescription = viewmodel.CategoryDescription;
            data.CategoryIsActive = viewmodel.CategoryIsActive;




            _dbcontext.Categorys.Add(data);
            _dbcontext.SaveChanges();
            return View();
        }

        [HttpPost]
        public IActionResult CategoryDelete(int id)
        {
            if (id == 0)
            {
                return Json("Category not valid");
            }
            var checkdata = _dbcontext.Categorys.Where(x => x.CategoryId == id).FirstOrDefault();
            if (checkdata != null)
            {
                _dbcontext.Remove(checkdata);
                _dbcontext.SaveChanges();
                return Ok();
            }

            return BadRequest();

        }





    }
}
