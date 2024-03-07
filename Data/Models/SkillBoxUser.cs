using Microsoft.AspNetCore.Identity;
using Data.Enums;

namespace Data.Models
{
    public class SkillBoxUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? WebsiteURL { get; set; }
        public string? WebsiteName { get; set; }
        public Gender Gender { get; set; }
        public City? City { get; set; }
    }
}
