using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkillBox.App.Services;
using System.Diagnostics;
using Contracts;
using Services;

namespace DiplomnaRabotaNet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseSeedService dbSeedService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public HomeController(DatabaseSeedService dbSeedService,
            ICategoryService categoryService,
            IUserService userService)
        {
            this.dbSeedService = dbSeedService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            //
            var categoryList = categoryService.GetAllCategoriesAsSelectListItem();
            var categoryCards = categoryService.GetAllCategoryCardDTOs();
            var userCards = userService.GetTopSkillersAsUserCardDTOs(8);
            var model = new HomeViewModel
            { 
                CategoryList = categoryList,
                CategoryCardDTOs = categoryCards,
                UserCardDTOs = userCards
            };
            return View(model);
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
