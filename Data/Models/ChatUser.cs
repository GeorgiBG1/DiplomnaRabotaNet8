namespace Data.Models
{
    public class ChatUser
    {
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; }
        public string ChatId { get; set; }
    }
}
