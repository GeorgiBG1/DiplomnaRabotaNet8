using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }public IActionResult Skillers()
        {
            return View();
        }
    }
}
