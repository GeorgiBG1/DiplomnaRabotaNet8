namespace Data.Models
{
    public class UserMessage : BaseEntity<string>
    {
        public UserMessage()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Content { get; set; }
        public string OwnerId { get; set; }
        public virtual SkillBoxUser Owner { get; set; }
        public string ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
