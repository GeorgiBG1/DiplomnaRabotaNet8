using Contracts;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace SkillBox.App.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string chatId, string message,
            UserManager<SkillBoxUser> userManager,
            IChatService chatService,
            IHttpContextAccessor httpContext)
        {
            var user = await userManager.GetUserAsync(httpContext.HttpContext!.User);
            var username = user!.UserName;
            await chatService.AddUserMessageToChatAsync(chatId, message, user);
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}