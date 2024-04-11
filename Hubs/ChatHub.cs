using Contracts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services;

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
            var chatUsers = await chatService.GetChatUsersByChatIdAsync(chatId);
            var messageDTO = await chatService.AddUserMessageToChatAsync(chatId, message, user);
            await Clients.Users(chatUsers.Select(cu => cu.UserId))
                .SendAsync("ReceiveMessage", username, messageDTO);
            //By default
            //await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
        public async Task ShowCurrentMessages(UserManager<SkillBoxUser> userManager,
            IHttpContextAccessor httpContext,
            IChatService chatService)
        {
            var user = await userManager.GetUserAsync(httpContext.HttpContext!.User);
            var messages = await chatService.GetLatestMessagesForUserAsync(user?.UserName!);
            await Clients.Caller.SendAsync("ReceiveCurrentMessages", messages);
        }
    }
}