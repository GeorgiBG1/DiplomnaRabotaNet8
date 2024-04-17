namespace Data.Models
{
    public class Notification : BaseEntity<string>
    {
        public string Content { get; set; }
        public string OwnerId { get; set; }
        public virtual SkillBoxUser Owner { get; set; }
        //TODO add more props
    }
}
