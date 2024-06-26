﻿using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Authorize(Roles = "Admin")]
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
            var model = offeringService.GetAllServicesAsServiceMiniDTOs();
            return View(model);
        }
        public IActionResult Skillers()
        {
            var model = userService.GetAllSkillerAsUserCardDTOs();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult GetUserInfoByUsername(string username)
        {
            var user = userService.GetUserAsUserDTOByUsername(username);
            if (user != null)
            {
                user.Career ??= "професия (-)";
                var userData = user;

                return Json(new { exists = true, user = userData });
            }
            return Json(new { exists = false });
        }
        public IActionResult DeleteService(int id)
        {
            var isDeleted = offeringService.DeleteService(id);
            if (!isDeleted)
            {
                return View("Error");
            }
            return RedirectToAction("Services", "Admin");
        }
        public IActionResult BlockUser(string id)
        {
            var isBlocked = userService.BlockUser(id);
            if (!isBlocked)
            {
                return View("Error");
            }
            return RedirectToAction("Skillers", "Admin");
        }
    }
}
