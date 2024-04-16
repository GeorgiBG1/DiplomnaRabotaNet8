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
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public ChatService(SkillBoxDbContext dbContext, IUserService userService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.mapper = mapper;
        }

        public Chat GetChatById(string id)
        {
            var chat = dbContext.Chats
                .Include(c => c.ChatUsers)
                .ThenInclude(cu => cu.User)
                .FirstOrDefault(c => c.Id == id);
            return chat!;
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
                .Include(c => c.Service)
                .Include(c => c.ChatUsers)
                .ThenInclude(cu => cu.User)
                .Where(c => c.ChatUsers
                .Select(cu => cu.UserId).Contains(id))
                .OrderBy(c => c.CreatedOn)
                .ToList();

            var model = chats.Select(mapper.Map<ChatMiniDTO>).ToList();
            return model;
        }
        public async Task<MessageDTO> AddUserMessageToChatAsync(string chatId, string message, SkillBoxUser user)
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
                    return mapper.Map<MessageDTO>(userMessage);
                }
            }
            return null!;
        }
        public async Task<List<ChatUser>> GetChatUsersByChatIdAsync(string id)
        {
            var chatUsers = await dbContext.ChatUsers
                .Where(cu => cu.ChatId == id)
                .Include(cu => cu.User)
                .ToListAsync();
            return chatUsers;
        }

        public async Task<List<MessageDTO>> GetLatestMessagesForUserAsync(string username)
        {
            var chats = dbContext.Chats
                .Include(c => c.ChatUsers)
                .ThenInclude(cu => cu.User)
                .Where(c => c.ChatUsers.Select(cu => cu.User.UserName).Contains(username))
                .Include(c => c.Messages)
                .ThenInclude(m => m.Owner);

            var messagesFromDb = await chats
                .SelectMany(c => c.Messages)
                .Where(m => m.Owner.UserName != username)
                .OrderByDescending(m => m.CreatedOn)
                .Take(3).ToListAsync();
            var messages = messagesFromDb.Select(mapper.Map<MessageDTO>).ToList();
            return messages;
        }
        public void AddNewChatUser(SkillBoxUser user, Chat chat)
        {
            var chatUser = new ChatUser
            {
                Chat = chat,
                User = user
            };
            dbContext.ChatUsers.Add(chatUser);
            dbContext.SaveChanges();
        }
        public void RemoveChatUser(SkillBoxUser user, Chat chat)
        {
            var chaUser = dbContext.ChatUsers.Where(cu => cu.Chat.Id == chat.Id)
                .Where(cu => cu.User.UserName == user.UserName)
                .FirstOrDefault();
            if (chaUser != null)
            {
                dbContext.ChatUsers.Remove(chaUser);
                dbContext.SaveChanges();
            }

        }
        public int GetChatsCount()
        {
            return dbContext.Chats.Count();
        }

        public Chat FindChatByUsers(string currentUsername, string usernameToConnect,  SkillBoxService service)
        {
            Chat chat;
            chat = dbContext.Chats
                .Where(c => c.ServiceId == service.Id)
                .Where(c => c.ChatUsers.Any(c => c.User.UserName == currentUsername)
                    && c.ChatUsers.Any(c => c.User.UserName == usernameToConnect))
                .FirstOrDefault()!;
            return chat;
        }

        public Chat CreateNewChat(string currentUsername, string usernameToConnect, SkillBoxService service)
        {
            var currentUser = userService.GetUserByUsername(currentUsername);
            var userToConnect = userService.GetUserByUsername(usernameToConnect);

            var chat = new Chat
            {
                Name = $"{currentUser.FirstName}, {userToConnect.FirstName}",
                Service = service
            };
            dbContext.Chats.Add(chat);

            var chatUser1 = new ChatUser
            {
                Chat = chat,
                User = currentUser
            };
            dbContext.ChatUsers.Add(chatUser1);
            
            var chatUser2 = new ChatUser
            {
                Chat = chat,
                User = userToConnect
            };
            dbContext.ChatUsers.Add(chatUser2);

            dbContext.SaveChanges();
            return chat;
        }
    }
}
