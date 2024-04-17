using Contracts;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly UserManager<SkillBoxUser> userManager;
        private readonly IChatService chatService;
        private readonly IUserService userService;
        private readonly IOfferingService offeringService;

        public ChatsController(UserManager<SkillBoxUser> userManager,
            IChatService chatService,
            IUserService userService,
            IOfferingService offeringService)
        {
            this.userManager = userManager;
            this.chatService = chatService;
            this.userService = userService;
            this.offeringService = offeringService;
        }
        public IActionResult Index()
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var userId = userManager.GetUserId(HttpContext.User);
            if (userId != null)
            {
                var chats = chatService.GetAllChatsMiniDTOsByUserId(userId);
                if (chats != null)
                {
                    return View(chats);
                }
            }
            return View("Error");
        }
        public IActionResult Chat(string id)
        {
            //Menu - Nav
            var userProfilePhoto = userService.GetUserProfilePhoto(User.Identity?.Name!);
            ViewData[nameof(userProfilePhoto)] = userProfilePhoto;
            //
            var userId = userManager.GetUserId(HttpContext.User);
            var chat = chatService.GetChatDTOById(id);
            if (userId != null && chat != null)
            {
                if (chat.ChatUsers.Select(cu => cu.UserId).Contains(userId))
                {
                    return View(chat);
                }
            }
            return View("Error");
        }
        public IActionResult DisplayChat(string chatId)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var chat = chatService.GetChatDTOById(chatId);
            if (userId != null && chat != null)
            {
                if (chat.ChatUsers.Select(cu => cu.UserId).Contains(userId))
                {
                    return PartialView("TinyChat", chat);
                }
            }
            return View("Error");
        }
        [HttpPost]
        public IActionResult CheckUserEmail(string email)
        {
            var user = userManager.FindByEmailAsync(email).Result;

            if (user != null)
            {
                var userData = new
                {
                    user.FirstName,
                    user.LastName,
                    user.ProfilePhoto,
                };

                return Json(new { exists = true, user = userData });
            }
            else
            {
                return Json(new { exists = false });
            }
        }
        [HttpPost]
        public IActionResult AddParticipant(string email, string chatId)
        {
            var user = userManager.FindByEmailAsync(email).Result;

            if (user != null)
            {
                var allChatUsers = chatService.GetChatUsersByChatIdAsync(chatId).Result.Select(cu => cu.User);
                if (allChatUsers.Any(u => u.UserName == user.UserName))
                {
                    return Json(new { success = false, message = "Този потребител вече е в този разговор!" });
                }
                var chat = chatService.GetChatById(chatId);
                if (chat == null)
                {
                    return Json(new { success = false, message = "Възникна грешка свързана с разговора!" });
                }
                chatService.AddNewChatUser(user, chat);
                return Json(new { success = true, message = "Успешно добавен потребител!" });
            }
            else
            {
                return Json(new { success = false, message = "Не е намерен потребител с такъв имейл адрес." });
            }
        }
        public IActionResult RemoveParticipant(string id)
        {
            var chat = chatService.GetChatById(id);
            var user = userManager.FindByNameAsync(User.Identity?.Name!).Result;
            if (chat == null || user == null)
            {
                return View("Error");
            }
            chatService.RemoveChatUser(user, chat);
            return RedirectToAction("Index", "Chats");
        }
        public IActionResult ConnectToChat(int id)
        {
            var service = offeringService.GetServiceById(id);
            if (service == null)
            {
                return View("Error");
            }
            var chat = chatService.FindChatByUsers(User.Identity?.Name!, service.Owner.UserName!, service);
            if (chat == null)
            {
                chat = chatService.CreateNewChat(User.Identity?.Name!, service.Owner.UserName!, service);
            }
            return RedirectToAction("Chat", new { id = chat.Id} );
        }
    }
}