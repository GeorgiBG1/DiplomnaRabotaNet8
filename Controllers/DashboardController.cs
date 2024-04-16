using AutoMapper;
using Contracts;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public DashboardController(
            IMapper mapper,
            IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }
        public IActionResult Index()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            return View();
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
    }
}
