using FeroCourse.Data;
using FeroCourse.Data.Dtos;
using FeroCourse.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FeroCourse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LookupMangController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LookupMangController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var datalist = _context.Lookups.OrderBy(x => x.Key)
                .Select(x => new LookupVM
                {
                    Id = x.Id,
                    Category = x.Category,
                    Key = x.Key,
                    Value = x.Value,
                    Description = x.Description,
                    
                })

                .ToList();
            ViewData["LookList"] = datalist;
            return View();
        }
        [HttpPost]
        public IActionResult LookupSave(LookupVM vm)
        {
            if (String.IsNullOrEmpty(vm.Key) || String.IsNullOrEmpty(vm.Value) || String.IsNullOrEmpty(vm.Category)) 
            {
                TempData["Message"] = "Lookup Saved Failed!";
                return RedirectToAction("Index");

            }

            var data = new Lookup
            {
                Category = vm.Category,
                Key = vm.Key,
                Value = vm.Value,
                Description = vm.Description,
            };
            _context.Lookups.Add(data);
            _context.SaveChanges();

            TempData["Message"] = "Lookup Saved Successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Lookupdelete(int id)
        {
            if (id > 0)
            {

                var checkdata = _context.Lookups.Where(x=>x.Id == id).FirstOrDefault();
                if (checkdata != null) {
                  _context.Lookups.Remove(checkdata);   
                  _context.SaveChanges(); 
                
                }
                return RedirectToAction("Index");
            }
            else return BadRequest();
        }

    }
}

