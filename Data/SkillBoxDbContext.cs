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
        public DbSet<ServiceUser> ServiceUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserServiceUsed>();
            //TODO fix foreign keys (User, Service -> UserService)
            //modelBuilder.Entity<UserChat>().HasKey(c => new { c.UserId, c.SkillBoxServiceId });
            //!!! latest !!! modelBuilder.Entity<ChatUser>().HasKey(x => new { x.ChatId, x.UserId });
            //modelBuilder.Entity<ChatUser>().ForeignKey(
            //    name: "FK_ChatSkillBoxUser_AspNetUsers_UsersId",
            //    column: x => x.UserId,
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
            //table.ForeignKey(
            //    name: "FK_ChatSkillBoxUser_Chats_ChatsId",
            //    column: x => x.ChatId,
            //    principalTable: "Chats",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}