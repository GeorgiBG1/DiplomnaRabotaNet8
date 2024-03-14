using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkillBox.App.Services;
using System.Diagnostics;

namespace DiplomnaRabotaNet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseSeedService dbSeedService;

        public HomeController(DatabaseSeedService dbSeedService)
        {
            this.dbSeedService = dbSeedService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DbSeedData(string id)
        {
            var secret = "123456";
            if (id == secret)
            {
                await dbSeedService.SeedAsync();
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
