using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    [PrimaryKey(nameof(ChatId), nameof(UserId))]
    public class ChatUser
    {
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; } //Participant
        public string ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
