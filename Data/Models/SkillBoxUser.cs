using Global_Constants;
using Microsoft.AspNetCore.Identity;
namespace Data.Models
{
    public class SkillBoxUser : IdentityUser
    {
        public SkillBoxUser()
        {
            ChatUsers = new HashSet<ChatUser>();
            Services = new HashSet<SkillBoxService>();
            Skills = new HashSet<Skill>();
            CreatedOn = DateTime.UtcNow;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }
        public string? WebsiteURL { get; set; }
        public string? WebsiteName { get; set; }
        public string ProfilePhoto { get; set; } = GlobalConstant.UserDefaultProfilePhoto;
        public string? Career { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
        public virtual ICollection<SkillBoxService> Services { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Offering> Offerings { get; set; }
    }
}
