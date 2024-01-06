using DiplomnaRabotaNet8.Data;
using Microsoft.AspNetCore.Identity;

namespace SkillBox.App.Data.Seeding
{
    public class SkillBoxUserRoleSeeder : ISeeder
    {
        private readonly SkillBoxDbContext context;

        public SkillBoxUserRoleSeeder(SkillBoxDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Roles.Add(new IdentityRole
                {
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                });

                context.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });
            }

            context.SaveChanges();
        }
    }
}