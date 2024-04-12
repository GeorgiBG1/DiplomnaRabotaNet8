using Contracts;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IUserService userService;
        private readonly IOfferingService offeringService;
        private readonly IChatService chatService;

        public AdminController(IAdminService adminService,
            IUserService userService,
            IOfferingService offeringService,
            IChatService chatService)
        {
            this.adminService = adminService;
            this.userService = userService;
            this.offeringService = offeringService;
            this.chatService = chatService;
        }
        public IActionResult Index()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var categories = adminService.GetAllCategoriesWithDetails();
            var services = adminService.GetTop10ServicesAsServiceInfoDTOs();
            var skillers = adminService.GetTop10SkillersAsUserInfoDTOs();
            var usersCount = userService.GetUsersCount();
            var skillersCount = userService.GetSkillersCount();
            var reviewsCount = offeringService.GetReviewsCount();
            var chatsCount = chatService.GetChatsCount();
            var completedServicesCount = offeringService.GetCompletedServicesCount();
            var skillsCount = userService.GetSkillsCount();
            var model = new AdminViewModel
            {
                Categories = categories,
                Services = services,
                Skillers = skillers,
                UsersCount = usersCount,
                SkillersCount = skillersCount,
                ReviewsCount = reviewsCount,
                ChatsCount = chatsCount,
                CompletedServicesCount = completedServicesCount,
                SkillsCount = skillsCount,
            };
            return View(model);
        }
        public IActionResult Services()
        {
            return View();
        }public IActionResult Skillers()
        {
            return View();
        }
    }
}
