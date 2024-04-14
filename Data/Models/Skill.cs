using ServiceStack.DataAnnotations;

namespace Data.Models
{
    public class Skill : BaseEntity<string>
    {
        public Skill()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }
        public virtual SkillLevel Level { get; set; }
        [Unique]
        public string UserId { get; set; }
        public virtual SkillBoxUser User { get; set; }
    }
}
