namespace Data.Models
{
    public class Chat : BaseEntity<string>
    {
        public Chat()
        {
            Id = Guid.NewGuid().ToString();
        }
        public virtual ICollection<UserMessage> Messages { get; set; }
    }
}
