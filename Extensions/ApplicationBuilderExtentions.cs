

using DiplomnaRabotaNet8.Data;
using SkillBox.App.Data.Seeding;
using System.Reflection;

namespace SkillBox.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider
                    .GetRequiredService<SkillBoxDbContext>();

                context.Database.EnsureCreated();

                Assembly.GetAssembly(typeof(SkillBoxDbContext))
                    .GetTypes()
                    .Where(type => typeof(ISeeder).IsAssignableFrom(type))
                    .Where(type => type.IsClass)
                    .Select(type => (ISeeder)serviceScope.ServiceProvider.GetRequiredService(type))
                    .ToList()
                    .ForEach(seeder => seeder.Seed().GetAwaiter().GetResult());
            }
        }
    }
}
