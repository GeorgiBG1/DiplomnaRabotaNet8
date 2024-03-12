using Data;
using Microsoft.AspNetCore.Mvc;
using DTOs.INPUT;

namespace SkillBox.App.Controllers
{
    public class ServicesController : Controller
    {
        private readonly SkillBoxDbContext dbContext;

        public ServicesController(SkillBoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult MyServices()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ServiceInDTO bindingModel)
        {
            return RedirectToAction("Index");
        }
    }
}
