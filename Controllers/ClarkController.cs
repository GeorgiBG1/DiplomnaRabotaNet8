using DiplomnaRabotaNet8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SkillBox.App.Controllers
{
    public class ClarkController : Controller
    {
        public IActionResult Index(string id)
        {
            if (id == null)
            {
                return View("Error");
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
