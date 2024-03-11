using DTOs.OUTPUT;

namespace Contracts
{
    public interface IChatService
    {
        public ChatDTO GetChatDTOById(string id);
        public ICollection<ChatMiniDTO> GetAllChatsMiniDTOsByUserId(string id);
    }
}
