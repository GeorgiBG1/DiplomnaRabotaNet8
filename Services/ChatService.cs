using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.OUTPUT;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ChatService : IChatService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly IMapper mapper;

        public ChatService(SkillBoxDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ChatDTO GetChatDTOById(string id)
        {
            var chat = dbContext.Chats
                .Include(c => c.Service)
                .Include(c => c.ChatUsers)
                .Include(c => c.Messages)
                .ThenInclude(m => m.Owner)
                .FirstOrDefault(c => c.Id == id);
            if (chat != null)
            {
                chat.Messages = chat.Messages.OrderBy(c => c.CreatedOn).ToArray();
            }
            var model = mapper.Map<ChatDTO>(chat);
            return model;
        }
        public ICollection<ChatMiniDTO> GetAllChatsMiniDTOsByUserId(string id)
        {
            var chats = dbContext.ChatUsers.Where(cu => cu.UserId == id)
                .Select(cu => cu.Chat).OrderBy(c => c.CreatedOn)
                .ToList();
            var model = chats.Select(chat => mapper.Map<ChatMiniDTO>(chat)).ToList();
            return model;
        }
        public async Task AddUserMessageToChat(string chatId, string message, SkillBoxUser user)
        {
            var chat = await dbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
            if (chat != null && user != null)
            {
                var userMessage = new UserMessage
                {
                    Content = message,
                    Chat = chat,
                    Owner = user
                };
                await dbContext.UserMessages.AddAsync(userMessage);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
