using Microsoft.AspNetCore.Mvc;

namespace EduPlatform.Controllers.MVCControllers.AdminPanel
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
