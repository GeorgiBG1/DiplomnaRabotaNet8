using AutoMapper;
using Contracts;
using Data.Models;
using DTOs.OUTPUT;
using Global_Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IDashboardService dashboardService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<SkillBoxUser> userManager;
        private readonly SignInManager<SkillBoxUser> signInManager;
        private readonly INotificationService notificationService;

        public DashboardController(
            IMapper mapper,
            IUserService userService,
            IDashboardService dashboardService,
            ICloudinaryService cloudinaryService,
            UserManager<SkillBoxUser> userManager,
            SignInManager<SkillBoxUser> signInManager,
            INotificationService notificationService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.dashboardService = dashboardService;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.notificationService = notificationService;
        }
        public IActionResult Index()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var model = new DashboardViewModel
            {
                MyServicesCount = dashboardService.GetMyServicesCount(User.Identity?.Name!),
                MyFavServicesCount = dashboardService.GetMyFavServicesCount(User.Identity?.Name!),
                MyFavSkillersCount = dashboardService.GetMyFavSkillersCount(User.Identity?.Name!),
                MyReviewsCount = dashboardService.GetMyReviewsCount(User.Identity?.Name!),
            };
            return View(model);
        }
        public IActionResult Saved()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            return View();
        }
        public IActionResult Reviews()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var myReviews = userService.GetAllMyReviews(User.Identity?.Name!);
            var model = myReviews.Select(mapper.Map<ReviewDTO>).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadProfilePhoto(IFormFile file)
        {
            var user = userService.GetUserByUsername(User.Identity?.Name!);

            if (user != null)
            {
                var imgURL = cloudinaryService.UploadFileAndGetURL(file, "UserProfilePhotos");
                if (imgURL != "none")
                {
                    await userService.SetProfilePhotoToUser(user, imgURL);
                    return Json(new { exists = true, userProfilePhoto = imgURL });
                }
                else
                {
                    return Json(new { exists = false, message = "Неуспешно качване на снимката!" });
                }
            }
            else
            {
                return Json(new { exists = false, message = "Неуспешно качване на снимката!" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SetProfilePhotoToDefault()
        {
            var user = userService.GetUserByUsername(User.Identity?.Name!);

            if (user != null)
            {
                var imgURL = await userService.SetProfilePhotoToUser(user, null!);
                return Json(new { exists = true, userProfilePhoto = imgURL });
            }
            else
            {
                return Json(new { exists = false, message = "Неуспешно качване на снимката!" });
            }
        }
        [HttpPost]
        public IActionResult AddUserToRoleSkiller()
        {
            var user = userService.GetUserByUsername(User.Identity?.Name!);

            if (user != null)
            {
                if (!User.IsInRole("Skiller"))
                {
                    userManager.AddToRoleAsync(user, "Skiller").GetAwaiter().GetResult();
                    notificationService.CreateNotification(GlobalConstant.UpdateUserPropsNotificationType, user);
                    signInManager.RefreshSignInAsync(user).GetAwaiter().GetResult();
                }
                return Json(new { exists = true, message = "Успешно станахте Skiller!" });
            }
            else
            {
                return Json(new { exists = false, message = "За съжаление възникна грешка!" });
            }
        }
    }
}