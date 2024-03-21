using Microsoft.AspNetCore.Identity;
using Data.Enums;
namespace Data.Models
{
    public class SkillBoxUser : IdentityUser
    {
        public SkillBoxUser()
        {
            ChatUsers = new HashSet<ChatUser>();
            Services = new HashSet<SkillBoxService>();
            Skills = new HashSet<Skill>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? WebsiteURL { get; set; }
        public string? WebsiteName { get; set; }
        public Gender Gender { get; set; }
        public City? City { get; set; }
        //TODO Career property
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
        public virtual ICollection<SkillBoxService> Services { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Offering> Offerings { get; set; }
    }
}
