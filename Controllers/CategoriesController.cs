﻿using Contracts;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;

        public CategoriesController(
            ICategoryService categoryService,
            IOfferingService offeringService)
        {
            this.categoryService = categoryService;
            this.offeringService = offeringService;
        }
        public IActionResult Index()
        {
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
            var category = categoryService.GetCategoryDTO(id);
            if (category == null)
            {
                return View("Error");
            }
            int servicesCount = offeringService.GetServicesCount(id);
            var categories = categoryService.GetCategoryMiniDTOs(7);
            var categoryServices = offeringService.GetServiceCardDTOs(6, 0, id);
            var model = new SingleCategoryViewModel
            {
                CategoryList = categories,
                Category = category,
                ServicesCount = servicesCount,
                ServiceCardDTOs = categoryServices
            };
            return View(model);
        }
    }
}
