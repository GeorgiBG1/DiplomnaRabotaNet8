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
            var chats = dbContext.Chats
                .Include(c => c.ChatUsers)
                .Where(c => c.ChatUsers
                .Select(cu => cu.UserId).Contains(id))
                .OrderBy(c => c.CreatedOn)
                .ToList();

            var model = chats.Select(mapper.Map<ChatMiniDTO>).ToList();
            return model;
        }
        public async Task AddUserMessageToChatAsync(string chatId, string message, SkillBoxUser user)
        {
            var chat = await dbContext.Chats.Include(c => c.ChatUsers)
                .FirstOrDefaultAsync(c => c.Id == chatId);
            if (chat != null && user != null)
            {
                if (chat.ChatUsers.Any(cu => cu.UserId == user.Id))
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
        public async Task<List<ChatUser>> GetChatUsersByChatIdAsync(string id)
        {
            var chatUsers = await dbContext.ChatUsers
                .Where(cu => cu.ChatId == id)
                .Include(cu => cu.User)
                .ToListAsync();
            return chatUsers;
        }

        public async Task<List<UserMessage>> GetLatestMessagesForUserAsync(string username)
        {
            var messages = await dbContext.UserMessages
                .Where(m => m.Owner.UserName == username)  // Assuming there's a field named "Username" in your Message entity
                .OrderByDescending(m => m.CreatedOn) // Assuming there's a field named "Timestamp" indicating when the message was sent
                .Take(3)
                .ToListAsync();
            return messages;
        }
    }
}
