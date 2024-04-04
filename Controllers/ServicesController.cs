using Data;
using Microsoft.AspNetCore.Mvc;
using DTOs.INPUT;
using Contracts;
using Services;
using Models;

namespace SkillBox.App.Controllers
{
    public class ServicesController : Controller
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICategoryService categoryService;
        private readonly IOfferingService offeringService;
        private readonly IImageService imageService;

        public ServicesController(SkillBoxDbContext dbContext,
            ICloudinaryService cloudinaryService,
            ICategoryService categoryService,
            IOfferingService offeringService,
            IImageService imageService)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
            this.categoryService = categoryService;
            this.offeringService = offeringService;
            this.imageService = imageService;
        }
        public IActionResult Index()
        {
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
        public IActionResult Details(int id)
        {
            return View();
        }
        public IActionResult MyServices()
        {
            var services = offeringService.GetServiceCardDTOs(8, 8);
            return View(services);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ServiceInDTO bindingModel)
        {
            //TODO Validations

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
    }
}
