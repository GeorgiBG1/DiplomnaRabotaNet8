using Data;
using Microsoft.AspNetCore.Mvc;
using DTOs.INPUT;
using Contracts;
using Services;
using Models;
using Microsoft.AspNetCore.Authorization;
using Data.Models;

namespace Controllers
{
    public class ServicesController : Controller
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;
        private readonly IUserService userService;
        private readonly IImageService imageService;

        public ServicesController(SkillBoxDbContext dbContext,
            ICloudinaryService cloudinaryService,
            ICategoryService categoryService,
            IOfferingService offeringService,
            IUserService userService,
            IImageService imageService)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
            this.categoryService = categoryService;
            this.offeringService = offeringService;
            this.userService = userService;
            this.imageService = imageService;
        }
        public IActionResult Index()
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var categories = categoryService.GetCategoryMiniDTOs(7);
            var services = offeringService.GetServiceCardDTOs(4);
            var model = new AllServiceViewModel
            {
                CategoryList = categories,
                ServiceCardDTOs = services
            };
            return View(model);
        }
        public IActionResult Service(int id)
        {
            //Menu - Nav
            var categoriesWithKids = categoryService.GetAllCategoryDTOs();
            ViewData[nameof(categoriesWithKids)] = categoriesWithKids;
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var service = offeringService.GetServiceDTO(id);
            if (service == null)
            {
                return View("Error");
            }
            var categories = categoryService.GetCategoryMiniDTOs(7);
            var popularServices = offeringService.GetTopServicesAsServiceCardDTOs(4, id);
            var model = new SingleServiceViewModel
            {
                CategoryList = categories,
                Service = service,
                ServiceCardDTOs = popularServices
            };
            return View(model);
        }
        public IActionResult Details()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var serviceStatuses = offeringService.GetAllServiceStatuses();
            var services = offeringService.GetAllSkillerServicesByUsername(User.Identity?.Name!);
            var model = new MyServicesViewModel
            {
                StatusesList = serviceStatuses,
                Services = services
            };
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            return View();
        }
        [Authorize(Roles = "Skiller, Admin")]
        public IActionResult MyServices()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var serviceStatuses = offeringService.GetAllServiceStatuses();
            var services = offeringService.GetAllSkillerServicesByUsername(User.Identity?.Name!);
            var model = new MyServicesViewModel
            {
                StatusesList = serviceStatuses,
                Services = services
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var model = new ServiceInDTO
            {
                Skills = userService.GetAllMySkills(User.Identity?.Name!).ToList(),
                Categories = categoryService.GetAllCategories().ToList(),
                Cities = offeringService.GetAllCities().ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(ServiceInDTO bindingModel)
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //

            if (ModelState.IsValid)
            {
                var a = bindingModel;
                if (bindingModel.Days.Count == 0)
                {
                    bindingModel.Days.Add(0);
                }
                var images = string.Empty;
                foreach (var img in bindingModel.ImageFiles)
                {
                    var imgURL = cloudinaryService.UploadFileAndGetURL(img, "ServiceImgs");
                    if (imgURL != "none")
                        images += $"{imgURL}|";
                }
                bindingModel.Images = images;
                offeringService.CreateService(bindingModel);
                return RedirectToAction("MyServices");
            }
            return View(bindingModel);
        }
        //Dynamic loading
        public IActionResult LoadMoreServices(int currentServicesCount)
        {
            var services = offeringService.GetServiceCardDTOs(4, currentServicesCount).ToList();
            if (services != null)
            {
                return Json(new { exists = true, services });
            }
            return Json(new { exists = false });
        }
    }
}
