namespace Data.Models
{
    public class UserServiceUsed
    {
        public int Id { get; set; }
        public int LaborServiceId { get; set; }
        public virtual SkillBoxService LaborService { get; set; }
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; }
        
    }
}