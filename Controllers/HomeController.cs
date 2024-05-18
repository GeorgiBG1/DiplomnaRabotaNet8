using Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Contracts;
using Services;
using System.Configuration;

namespace Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseSeedService dbSeedService;
        private readonly IConfiguration configuration;
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;
        private readonly IAdminService adminService;
        private readonly IUserService userService;

        public HomeController(DatabaseSeedService dbSeedService,
            IConfiguration configuration,
            ICategoryService categoryService,
            IOfferingService offeringService,
            IAdminService adminService,
            IUserService userService)
        {
            this.dbSeedService = dbSeedService;
            this.configuration = configuration;
            this.categoryService = categoryService;
            this.offeringService = offeringService;
            this.adminService = adminService;
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
            var subsection = configuration
                .GetRequiredSection("DatabaseSeed");
            var secret = subsection["Secret"];
            if (id == secret)
            {
                await dbSeedService.SeedAsync();
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        public IActionResult BecomeSkiller() => View();
        public IActionResult Privacy() => View();
        public IActionResult NoResults()
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            return View();
        }
        public IActionResult Contacts()
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var adminList = adminService.GetAllAdminContacts();
            return View(adminList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
