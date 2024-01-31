namespace DiplomnaRabotaNet8.Data.Models
{
    public class UserService
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; }
        public int LaborServiceId { get; set; }
        public virtual SkillBoxService LaborService { get; set; }
    }
}