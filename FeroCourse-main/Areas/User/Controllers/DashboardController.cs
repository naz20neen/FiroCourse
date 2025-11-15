using FeroCourse.Areas.Admin.Controllers;
using FeroCourse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

namespace FeroCourse.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = StaticMessages.User)]
    public class DashboardController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }
    }
}
