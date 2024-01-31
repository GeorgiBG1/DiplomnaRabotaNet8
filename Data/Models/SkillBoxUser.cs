using Microsoft.AspNetCore.Identity;
using SkillBox.App.Data.Enums;

namespace DiplomnaRabotaNet8.Data.Models
{
    public class SkillBoxUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? WebsiteURL { get; set; }
        public string? WebsiteName { get; set; }
        public DateOnly DateOfBirth {  get; set; }
        public virtual int Age => DateTime.UtcNow.Year - DateOfBirth.Year;
        public City City { get; set; }
    }
}
