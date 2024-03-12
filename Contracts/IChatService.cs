using Data.Models;
using DTOs.OUTPUT;

namespace Contracts
{
    public interface IChatService
    {
        public ChatDTO GetChatDTOById(string id);
        public ICollection<ChatMiniDTO> GetAllChatsMiniDTOsByUserId(string id);
        public Task AddUserMessageToChat(string chatId, string message, SkillBoxUser user);
    }
}
