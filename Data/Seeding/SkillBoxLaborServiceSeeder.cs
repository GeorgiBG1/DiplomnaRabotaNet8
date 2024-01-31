using DiplomnaRabotaNet8.Data;
using DiplomnaRabotaNet8.Data.Models;

namespace SkillBox.App.Data.Seeding
{
    public class SkillBoxLaborServiceSeeder : ISeeder
    {
        private readonly SkillBoxDbContext context;

        public SkillBoxLaborServiceSeeder(SkillBoxDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.LaborServices.Any())
            {
                context.LaborServices.Add(new SkillBoxService
                {
                    Name = "Pochistvam bani",
                    AuthorName = "Unknown",
                    Description = "None",
                    CategoryId = 7,
                    MainImage = "https://c0.wallpaperflare.com/preview/283/339/827/adult-bathroom-bowl-chambermaid.jpg",
                    Images = "None",
                    Price = 0,
                    Discount = 0
                });

                context.LaborServices.Add(new SkillBoxService
                {
                    Name = "Podstrigvam hubavo",
                    AuthorName = "Emi",
                    Description = "Ela i si izberi pricheska",
                    CategoryId = 4,
                    MainImage = "https://icieducation.co.uk/blog/wp-content/uploads/2021/07/guide-to-becoming-a-hairdresser.jpg",
                    Images = "None",
                    Price = 7m,
                    Discount = 0.45m
                });

                context.LaborServices.Add(new SkillBoxService
                {
                    Name = "Az sum vashiq ikonom",
                    AuthorName = "Nqkoy si",
                    Description = "Imam 10 godini staj v tazi oblast!",
                    CategoryId = 3,
                    MainImage = "https://www.butlerforyou.com/wp-content/uploads/2018/01/Butler-Service.jpg",
                    Images = "None",
                    Price = 0,
                    Discount = 0
                });
            }

            context.SaveChanges();
        }
    }
}
