using Data;
using Data.Models;
using Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SkillBox.App.Services
{
    public class DatabaseSeedService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly UserManager<SkillBoxUser> userManager;

        public DatabaseSeedService(SkillBoxDbContext dbContext,
            UserManager<SkillBoxUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task SeedAsync()
        {
            if (!await dbContext.Categories.AnyAsync())
            {
                //Users
                var user1 = new SkillBoxUser
                {
                    UserName = "test123",
                    NormalizedUserName = "TEST123",
                    Email = "pesho@peshov.com",
                    NormalizedEmail = "PESHO@PESHOV.COM",
                    EmailConfirmed = true,
                    FirstName = "Pesho",
                    LastName = "Peshov",
                    Gender = Gender.Male,
                    City = City.Ruse,
                };
                await userManager.CreateAsync(user1, "123456");

                var user2 = new SkillBoxUser
                {
                    UserName = "emi43",
                    NormalizedUserName = "EMI43",
                    Email = "emi43@petrova.com",
                    NormalizedEmail = "EMI43@PETROVA.COM",
                    EmailConfirmed = true,
                    FirstName = "Emi",
                    LastName = "Petrova",
                    Gender = Gender.Female,
                    City = City.Varna,
                    WebsiteName = "NewHorizons"
                };
                await userManager.CreateAsync(user2, "123456");

                //Categories
                var category1 = new Category()
                {
                    Name = "Храна"
                };
                await dbContext.Categories.AddAsync(category1);

                var categories2 = new Category()
                {
                    Name = "Топла и домашна",
                    ParentCategory = category1,
                };
                await dbContext.Categories.AddAsync(categories2);

                //Services
                var service1 = new SkillBoxService()
                {
                    Name = "Правя добра баница, опитайте!",
                    Description = "Бързо, лесно, вкусно и домашно.",
                    Category = categories2,
                    MainImage = "None",
                    Images = "None",
                    Price = 0,
                    Discount = 0,
                    Owner = user1,
                    City = City.Dobrich
                };
                await dbContext.Services.AddAsync(service1);

                //ServiceUsers
                var serviceUser1 = new ServiceUser
                {
                    Service = service1,
                    User = user1,
                };
                await dbContext.ServiceUsers.AddAsync(serviceUser1);

                //Chats
                var chat1 = new Chat()
                {
                    Name = $"{user1.FirstName}, {user2.FirstName}",
                    Service = service1
                };
                await dbContext.Chats.AddAsync(chat1);

                //UserMessages
                var userMessage1 = new UserMessage()
                {
                    Owner = user1,
                    Chat = chat1,
                    Content = "Пиша в новосъздадения чат!"
                };
                await dbContext.UserMessages.AddAsync(userMessage1);

                var userMessage2 = new UserMessage()
                {
                    Owner = user2,
                    Chat = chat1,
                    Content = "Аз ти отговарям!"
                };
                await dbContext.UserMessages.AddAsync(userMessage2);

                //ChatUsers
                var chatUser1 = new ChatUser
                {
                    Chat = chat1,
                    User = user1, //Participant
                };
                await dbContext.ChatUsers.AddAsync(chatUser1);

                var chatUser2 = new ChatUser
                {
                    Chat = chat1,
                    User = user2, //Participant2
                };
                await dbContext.ChatUsers.AddAsync(chatUser2);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}