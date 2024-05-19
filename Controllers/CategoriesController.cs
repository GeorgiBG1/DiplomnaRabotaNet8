using Contracts;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;
        private readonly IUserService userService;

        public CategoriesController(
            ICategoryService categoryService,
            IOfferingService offeringService,
            IUserService userService)
        {
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
            var categoryList = categoryService.GetCategoryMiniDTOs(7);
            var categories = categoryService.GetAllCategoryCardDTOs();
            var model = new AllCategoryViewModel
            {
                CategoryList = categoryList,
                Categories = categories
            };
            return View(model);
        }
        public IActionResult Category(int id)
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var category = categoryService.GetCategoryDTO(id);
            if (category == null)
            {
                return View("Error");
            }
            var cities = categoryService.GetAllCities();
            int servicesCount = offeringService.GetServicesCount(id);
            var categories = categoryService.GetCategoryMiniDTOs(7);
            var categoryServices = offeringService.GetServiceCardDTOs(6, 0, id);
            var model = new SingleCategoryViewModel
            {
                Cities = cities.ToList(),
                CategoryList = categories,
                Category = category,
                ServicesCount = servicesCount,
                ServiceCardDTOs = categoryServices
            };
            return View(model);
        }
    }
}
