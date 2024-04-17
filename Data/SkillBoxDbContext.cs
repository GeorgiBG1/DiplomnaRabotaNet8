using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Data
{
    public class SkillBoxDbContext : IdentityDbContext<SkillBoxUser, IdentityRole, string>
    {
        public SkillBoxDbContext(DbContextOptions<SkillBoxDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SkillBoxService> Services { get; set; }
        public DbSet<Offering> Offerings { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ServiceStatus> ServiceStatuses { get; set; }
        public DbSet<SkillLevel> SkillLevels { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}