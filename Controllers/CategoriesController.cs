using Microsoft.AspNetCore.Mvc;

namespace SkillBox.App.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
