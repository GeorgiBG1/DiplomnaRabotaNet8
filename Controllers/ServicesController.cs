using Microsoft.AspNetCore.Mvc;
using DTOs.INPUT;
using Contracts;
using Models;
using Microsoft.AspNetCore.Authorization;
using CacheConfiguration;
using Microsoft.AspNetCore.Identity;
using Data.Models;
using Global_Constants;
using DTOs.OUTPUT;

namespace Controllers
{
    public class ServicesController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;
        private readonly UserManager<SkillBoxUser> userManager;
        private readonly IUserService userService;
        private readonly INotificationService notificationService;

        public ServicesController(
            UserManager<SkillBoxUser> userManager,
            ICloudinaryService cloudinaryService,
            ICategoryService categoryService,
            IOfferingService offeringService,
            IUserService userService,
            INotificationService notificationService)
        {
            this.cloudinaryService = cloudinaryService;
            this.categoryService = categoryService;
            this.offeringService = offeringService;
            this.userManager = userManager;
            this.userService = userService;
            this.notificationService = notificationService;
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
                RatingStats = service.RatingStats,
                ServiceCardDTOs = popularServices
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetUserComment(int serviceId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
            {
                TempData["RatingError"] = "Невалидна стойност на оценката. Моля, дайте оценка между 1 и 5 звезди.";
                return RedirectToAction("Service", new { id = serviceId });
            }

            var user = userService.GetUserByUsername(User.Identity?.Name!);

            if (user != null && comment != null)
            {
                userService.AddUserComment(serviceId, user, comment, rating);
            }
            return RedirectToAction("Service", new { id = serviceId });
        }
        [Authorize(Roles = "Skiller")]
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

        [Authorize(Roles = "Skiller")]
        [HttpGet]
        public IActionResult Create()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //

            //View - Collection
            CacheData.Skills = userService.GetAllMySkills(User.Identity?.Name!).ToList();
            CacheData.Categories = categoryService.GetAllCategories().ToList();
            CacheData.Cities = offeringService.GetAllCities().ToList();
            //
            var model = new ServiceInDTO();
            return View(model);
        }
        [Authorize(Roles = "Skiller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceInDTO bindingModel)
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //

            var skills = CacheData.Skills;

            if (ModelState.IsValid)
            {
                var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();

                bindingModel.Skills = skills;
                bindingModel.Category = categoryService.GetCategoryById(bindingModel.CategoryId);
                bindingModel.City = userService.GetCityById(bindingModel.CityId);
                bindingModel.User = user;
                bindingModel.Status = offeringService.GetServiceStatusById(1);

                var images = string.Empty;
                foreach (var img in bindingModel.ImageFiles)
                {
                    var imgURL = cloudinaryService.UploadFileAndGetURL(img, "ServiceImgs");
                    if (imgURL != "none")
                    {
                        if (img != bindingModel.ImageFiles.ElementAt(0))
                        {
                            images += $"{imgURL}|";
                        }
                        else
                        {
                            bindingModel.MainImage = imgURL;
                        }
                    }
                    else
                    {
                        return View(bindingModel);
                    }
                }
                bindingModel.Images = images;
                offeringService.CreateService(bindingModel);
                notificationService.CreateNotification(GlobalConstant.CreateServiceNotificationType, user!);
                CacheData.Categories = null!;
                CacheData.Cities = null!;
                CacheData.Skills = null!;
                return RedirectToAction("MyServices");
            }
            return View(bindingModel);
        }
        [Authorize(Roles = "Skiller")]
        public IActionResult Edit(int id)
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //

            //View - Collection
            CacheData.Skills = userService.GetAllMySkills(User.Identity?.Name!).ToList();
            CacheData.Categories = categoryService.GetAllCategories().ToList();
            CacheData.Cities = offeringService.GetAllCities().ToList();
            CacheData.ServiceStatuses = offeringService.GetAllServiceStatuses().ToList();
            //
            var model = offeringService.GetServiceAsServiceUpdateDTO(id);
            return View(model);
        }
        [Authorize(Roles = "Skiller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceUpdateDTO bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var category = categoryService.GetCategoryById(bindingModel.CategoryId);
                var city = userService.GetCityById(bindingModel.CityId);
                var status = offeringService.GetServiceStatusById(bindingModel.StatusId + 1);
                bindingModel.Category = category;
                bindingModel.City = city;
                bindingModel.Status = status;
                bindingModel.Skills = CacheData.Skills;
                offeringService.UpdateService(bindingModel);
                notificationService.CreateNotification(GlobalConstant.UpdateServiceNotificationType, user!);
                CacheData.Categories = null!;
                CacheData.Cities = null!;
                CacheData.Skills = null!;
                CacheData.Images = null!;
                CacheData.ServiceStatuses = null!;
                CacheData.SelectedStatus = null!;
                return RedirectToAction("Details");
            }
            bindingModel.Status = CacheData.SelectedStatus;
            bindingModel.Images = CacheData.Images;
            return View(bindingModel);
        }
        [Authorize(Roles = "Skiller")]
        public IActionResult Delete(int id)
        {
            var isDeleted = offeringService.DeleteService(id);
            if (!isDeleted)
            {
                return View("Error");
            }
            return RedirectToAction("Details");
        }
        [Authorize(Roles = "Skiller")]
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
        //Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchForService(string searchWords, int categoryId = 0)
        {
            if (searchWords != null)
            {
                var serviceId = offeringService.GetServiceIdBySearchWords(searchWords, categoryId);
                if (serviceId != 0)
                {
                    return RedirectToAction("Service", new { id = serviceId });
                }
            }
            return RedirectToAction("NoResults", "Home");
        }
        //Dynamic search
        [HttpPost]
        public IActionResult GetServicesBySearchInput(string userInput)
        {
            if (userInput != null)
            {
                var services = offeringService.GetServicesBySearchWords(userInput, 3);
                if (services.Count != 0)
                {
                    var servicesWithLongNameServices = services.Where(s => s.Name.Length > 30).ToList();
                    if (servicesWithLongNameServices.Count != 0)
                    {
                        servicesWithLongNameServices.ForEach(s => s.Name = $"{s.Name![..27]}...");
                        foreach (var service in services.Where(s => s.Name.Length > 30))
                        {
                            service.Name = servicesWithLongNameServices.FirstOrDefault(s => s.Id == service.Id)!.Name;
                        }
                    }

                    var servicesList = services.ToList();
                    var servicesResult = new List<object>();
                    for (int i = 0; i < servicesList.Count; i++)
                    {
                        if (servicesList[i].ReviewAvgCoef.Equals(double.NaN))
                        {
                            servicesList[i].ReviewAvgCoef = 0;
                        }
                        var service = new
                        {
                            Id = servicesList[i].Id,
                            Name = servicesList[i].Name,
                            Category = servicesList[i].CategoryName,
                            MainImage = servicesList[i].MainImage,
                            Price = servicesList[i].Price.ToString(),
                            SkillerName = servicesList[i].AuthorName,
                            ReviewAvgCoef = servicesList[i].ReviewAvgCoef.ToString("F2"),
                            ReviewsCount = servicesList[i].ReviewsCount.ToString()
                        };
                        servicesResult.Add(service);                       
                    }
                    return Json(new { exists = true, services = servicesResult });
                }
            }
            return Json(new { exists = false });
        }
    }
}