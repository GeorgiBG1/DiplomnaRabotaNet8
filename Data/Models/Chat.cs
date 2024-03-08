using Data.Models;

namespace Data.Models
{
    public class Chat : BaseEntity<string>
    {
        public Chat()
        {
            Id = Guid.NewGuid().ToString();
            ChatUsers = new HashSet<ChatUser>();
        }
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
        public virtual ICollection<UserMessage> Messages { get; set; }
    }
}
