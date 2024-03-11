using Contracts;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SkillBox.App.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly UserManager<SkillBoxUser> userManager;
        private readonly IChatService chatService;

        public ChatsController(UserManager<SkillBoxUser> userManager,
            IChatService chatService)
        {
            this.userManager = userManager;
            this.chatService = chatService;
        }
        public IActionResult Index()
        {
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
    }
}