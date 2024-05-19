﻿using Contracts;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    public class SkillersController : Controller
    {
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;

        public SkillersController(
            IUserService userService,
            ICategoryService categoryService,
            IOfferingService offeringService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
            this.offeringService = offeringService;
        }
        public IActionResult Index()
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var cities = categoryService.GetAllCities();
            var categoryList = categoryService.GetCategoryMiniDTOs(7);
            var skillersCount = userService.GetSkillersCount();
            var skillers = userService.GetSkillerCardDTOs(5);
            var model = new AllSkillerViewModel
            {
                Cities = cities.ToList(),
                CategoryList = categoryList,
                SkillersCount = skillersCount,
                Skillers = skillers
            };
            return View(model);
        }
        public IActionResult Skiller(string id)
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var skiller = userService.GetSkillerDTO(id);
            if (skiller == null)
            {
                return View("Error");
            }
            var categories = categoryService.GetCategoryMiniDTOs(7);
            var skillerServices = userService.GetSkillerServicesAsServiceCardDTOs(id, 3);
            var model = new SingleSkillerViewModel
            {
                CategoryList = categories,
                Skiller = skiller,
                ServiceCardDTOs = skillerServices
            };
            return View(model);
        }
    }
}
