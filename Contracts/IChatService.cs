using Data.Models;
using DTOs.OUTPUT;

namespace Contracts
{
    public interface IChatService
    {
        public ChatDTO GetChatDTOById(string id);
        public ICollection<ChatMiniDTO> GetAllChatsMiniDTOsByUserId(string id);
        public Task<MessageDTO> AddUserMessageToChatAsync(string chatId, string message, SkillBoxUser user);
        public Task<List<ChatUser>> GetChatUsersByChatIdAsync(string id);
        public Task<List<MessageDTO>> GetLatestMessagesForUserAsync(string username);
    }
}
