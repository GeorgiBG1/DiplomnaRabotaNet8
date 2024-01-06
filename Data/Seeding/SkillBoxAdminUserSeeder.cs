using DiplomnaRabotaNet8.Data;
using DiplomnaRabotaNet8.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace SkillBox.App.Data.Seeding
{
    public class SkillBoxAdminUserSeeder : ISeeder
    {
        private readonly UserManager<SkillBoxUser> userManager;

        public SkillBoxAdminUserSeeder(UserManager<SkillBoxUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            var user = new SkillBoxUser
            {
                UserName = "root",
                Email = "root@eventures.com",
                FirstName = "Root",
                LastName = "Root"
            };

            var result = await userManager.CreateAsync(user, "root");
        }
    }
}
