using Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Contracts;
using Services;

namespace Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseSeedService dbSeedService;
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;
        private readonly IUserService userService;

        public HomeController(DatabaseSeedService dbSeedService,
            ICategoryService categoryService,
            IOfferingService offeringService,
            IUserService userService)
        {
            this.dbSeedService = dbSeedService;
            this.categoryService = categoryService;
            this.offeringService = offeringService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var skillersCount = userService.GetSkillersCount();
            var servicesCount = offeringService.GetServicesCount();
            var positiveViewsCount = offeringService.GetPositiveReiewsCount();
            var skillsCount = userService.GetSkillsCount();
            var categoryList = categoryService.GetAllCategoriesAsSelectListItem();
            var categoryCards = categoryService.GetAllCategoryCardDTOs();
            var userCards = userService.GetTopSkillersAsUserCardDTOs(8);
            var serviceCards = offeringService.GetTopServicesAsServiceCardDTOs(8);
            var model = new HomeViewModel
            {
                SkillersCount = skillersCount,
                ServicesCount = servicesCount,
                PositiveReviewsCount = positiveViewsCount,
                SkillsCount = skillsCount,
                CategoryList = categoryList,
                CategoryCardDTOs = categoryCards,
                UserCardDTOs = userCards,
                ServiceCardDTOs = serviceCards
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

        public IActionResult BecomeSkiller() => View();
        public IActionResult Privacy() => View();
        public IActionResult Help() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
