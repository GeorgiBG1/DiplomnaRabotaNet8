using DiplomnaRabotaNet8.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiplomnaRabotaNet8.Data
{
    public class SkillBoxDbContext : IdentityDbContext<SkillBoxUser, IdentityRole, string>
    {
        public SkillBoxDbContext(DbContextOptions<SkillBoxDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SkillBoxService> LaborServices { get; set; }
        public DbSet<SkillBoxUser> SkillBoxUsers { get; set; }
        public DbSet<UserService> UserServices { get; set; }
    }
}
