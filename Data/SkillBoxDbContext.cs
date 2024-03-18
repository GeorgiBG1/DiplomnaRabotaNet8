using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

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
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
    }
}