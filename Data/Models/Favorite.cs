namespace Data.Models
{
    public class Favorite : BaseEntity<int>
    {
        public bool? IsSkiller { get; set; }
        public string? SkillerId { get; set; }
        public virtual SkillBoxUser? Skiller { get; set; }
        public bool? IsService { get; set; }
        public int? ServiceId { get; set; }
        public virtual SkillBoxService Service { get; set; }
    }
}
