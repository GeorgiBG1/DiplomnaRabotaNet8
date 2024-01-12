using DiplomnaRabotaNet8.Data;
using DiplomnaRabotaNet8.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace SkillBox.App.Data.Seeding
{
    public class SkillBoxCategorySeeder : ISeeder
    {
        private readonly SkillBoxDbContext context;

        public SkillBoxCategorySeeder(SkillBoxDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category
                {
                    Name = "Electronics"
                });

                context.Categories.Add(new Category
                {
                    Name = "Logistics services"
                });

                context.Categories.Add(new Category
                {
                    Name = "Household services"
                });

                context.Categories.Add(new Category
                {
                    Name = "Hairdressing services"
                });

                context.Categories.Add(new Category
                {
                    Name = "Barber services"
                });

                var cleaningService = new Category //Parent
                {
                    Name = "Cleaning services",
                };
                context.Categories.Add(cleaningService);

                context.Categories.Add(new Category
                {
                    Name = "Bathroom cleaning",
                    ParentCategory = cleaningService
                });

                context.Categories.Add(new Category
                {
                    Name = "Chimney cleaning",
                    ParentCategory = cleaningService
                });
            }

            context.SaveChanges();
        }
    }
}
