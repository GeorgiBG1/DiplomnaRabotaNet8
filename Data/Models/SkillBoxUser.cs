using Microsoft.AspNetCore.Identity;

namespace DiplomnaRabotaNet8.Data.Models
{
    public class SkillBoxUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? WebsiteURL { get; set; }
        public string? WebsiteName { get; set; }
    }
}
