using Data.Models;

namespace DTOs.OUTPUT
{
    public class ChatDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ServiceName { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
        public virtual ICollection<UserMessage> Messages { get; set; }
    }
}
