using System.Diagnostics;
using FeroCourse.Data;
using FeroCourse.Data.Entities;
using FeroCourse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeroCourse.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
    {
        _logger = logger;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Contact()
    {
        var lookups = _context.Lookups
            .Where(x => x.Category == "Contact")
            .ToList();
        var contactdata = new
        {
            Map= lookups.FirstOrDefault(x=>x.Key == "Google Map")?.Value ??"",
            Address = lookups.Where(x => x.Key == "Address").Select(x => x.Value).ToList(),
            Email = lookups.Where(x => x.Key == "Email").Select(x => x.Value).ToList(),
            Phone = lookups.Where(x => x.Key == "Phone").Select(x => x.Value).ToList(),
            OfficeHour = lookups.Where(x => x.Key == "OfficeHour").Select(x => x.Value).ToList()

        };
        return View(contactdata);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
