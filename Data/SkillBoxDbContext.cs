using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillBox.App.Data.Models;

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
        public DbSet<SkillBoxService> LaborServices { get; set; }
        //public DbSet<UserServiceUsed> UserServicesUsed { get; set; } TODO
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserServiceUsed>();
            //TODO fix foreign keys (User, Service -> UserService)
            modelBuilder.Entity<UserChat>().HasKey(c => new { c.UserId, c.SkillBoxServiceId });
            base.OnModelCreating(modelBuilder);
        }
    }
}