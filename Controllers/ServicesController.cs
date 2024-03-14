using Data;
using Microsoft.AspNetCore.Mvc;
using DTOs.INPUT;
using Contracts;
using Services;

namespace SkillBox.App.Controllers
{
    public class ServicesController : Controller
    {
        private readonly SkillBoxDbContext dbContext;
        private ICloudinaryService cloudinaryService;
        private readonly IOfferingService offeringService;
        private readonly IImageService imageService;

        public ServicesController(SkillBoxDbContext dbContext,
            ICloudinaryService cloudinaryService,
            IOfferingService offeringService,
            IImageService imageService)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
            this.offeringService = offeringService;
            this.imageService = imageService;
        }
        public IActionResult Index()
        {
            var services = offeringService.GetServiceCardDTOs(5);
            return View(services);
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult MyServices()
        {
            return View();
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
                var imgURL = cloudinaryService.UploadFileAndGetURL(img);
                if (imgURL != "none")
                    images += $"{imgURL}|";
            }
            bindingModel.Images = images;
            offeringService.CreateService(bindingModel);
            return RedirectToAction("MyServices");
        }
    }
}
