using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.OUTPUT;
using Global_Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly UserManager<SkillBoxUser> userManager;
        private readonly IOfferingService offeringService;
        private readonly INotificationService notificationService;
        private readonly IMapper mapper;

        public UserService(SkillBoxDbContext dbContext,
            UserManager<SkillBoxUser> userManager,
            IOfferingService offeringService,
            INotificationService notificationService,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.offeringService = offeringService;
            this.notificationService = notificationService;
            this.mapper = mapper;
        }
        public SkillBoxUser GetUserByUsername(string username)
        {
            return dbContext.Users
                .Include(u => u.Gender)
                .Include(u => u.City)
                .Include(u => u.Skills)
                .ThenInclude(s => s.Level)
                .FirstOrDefault(u => u.UserName == username)!;
        }
        public UserInfoDTO GetUserAsUserDTOByUsername(string username)
        {
            var user = dbContext.Users
            .Include(u => u.Gender)
            .Include(u => u.Services)
            .ThenInclude(s => s.Reviews)
            .Include(u => u.City)
            .Include(u => u.Skills)
            .ThenInclude(s => s.Level)
            .FirstOrDefault(u => u.UserName == username)!;
            var model = mapper.Map<UserInfoDTO>(user);
            return model;
        }
        public UserDTO GetSkillerDTO(string username)
        {
            var skiller = dbContext.Users.Where(u => u.UserName == username)
                .Include(u => u.City)
                .Include(u => u.Gender)
                .Include(u => u.Skills)
                .ThenInclude(s => s.Level)
                .FirstOrDefault();
            var model = mapper.Map<UserDTO>(skiller);
            return model;
        }
        public ICollection<UserCardDTO> GetSkillerCardDTOs(int count = 1, int skipCount = 0)
        {
            var skillers = dbContext.Users
                .Where(u => u.Services.Count != 0)
                .Include(u => u.City)
                .Include(u => u.Skills)
                .Include(u => u.Services.Where(s => !s.IsDeleted))
                .ThenInclude(s => s.Reviews)
                .OrderByDescending(u => u.Services.SelectMany(s => s.Reviews!).Count())
                .Skip(skipCount)
                .Take(count).ToList();
            var model = skillers.Select(mapper.Map<UserCardDTO>).ToList();
            return model;
        }
        public ICollection<UserCardDTO> GetAllSkillerAsUserCardDTOs()
        {
            var skillers = dbContext.Users
                .Where(u => u.Services.Count != 0)
                .Include(u => u.City)
                .Include(u => u.Services.Where(s => !s.IsDeleted))
                .ThenInclude(s => s.Reviews)
                .OrderByDescending(u => u.CreatedOn)
                .ToList();
            var model = skillers.Select(mapper.Map<UserCardDTO>).ToList();
            return model;
        }
        public ICollection<ServiceCardDTO> GetSkillerServicesAsServiceCardDTOs(string username, int count = 1)
        {
            var services = dbContext.Services
                .Where(s => !s.IsDeleted)
                .Where(s => s.Owner.UserName == username)
                .OrderByDescending(s => s.Id)
                .Include(s => s.Category)
                .Take(count).ToList();
            var servicesWithLongNameServices = services.Where(s => s.Name.Length > 50).ToList();
            if (servicesWithLongNameServices.Count != 0)
            {
                servicesWithLongNameServices.ForEach(s => s.Name = $"{s.Name![..47]}...");
                foreach (var service in services.Where(s => s.Name.Length > 50))
                {
                    service.Name = servicesWithLongNameServices.FirstOrDefault(s => s.Id == service.Id)!.Name;
                }

            }
            var model = services.Select(mapper.Map<ServiceCardDTO>).ToList();
            return model;
        }
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1)
        {
            var skillers = dbContext.Users
                .Where(u => u.Services.Count != 0)
                .Include(u => u.City)
                .Include(u => u.Skills)
                .Include(u => u.Services.Where(s => !s.IsDeleted))
                .ThenInclude(s => s.Reviews)
                .OrderByDescending(u => u.Services.SelectMany(s => s.Reviews!).Count())
                .Take(count).ToList();
            var model = skillers.Select(mapper.Map<UserCardDTO>).ToList();
            return model;
        }
        public ICollection<Skill> GetAllMySkills(string username)
        {
            return dbContext.Skills.Where(s => s.User.UserName == username).ToList();
        }
        public ICollection<Review> GetAllMyReviews(string username)
        {
            var reviews = dbContext.Reviews
                .Where(r => r.User.UserName == username)
                .ToList();
            return reviews;
        }
        public bool BlockUser(string username)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                {
                    user.LockoutEnd = null;
                }
                else
                {
                    user.LockoutEnd = DateTime.Now.Add(TimeSpan.FromDays(1));
                }
                userManager.UpdateAsync(user).GetAwaiter().GetResult();
                return true;
            }
            return false;
        }
        public City GetCityById(int id)
        {
            return dbContext.Cities.FirstOrDefault(c => c.Id == id)!;
        }
        public int GetUsersCount()
        {
            return dbContext.Users.Count();
        }
        public int GetSkillersCount()
        {
            return dbContext.Users
                .Include(u => u.Services)
                .Where(u => u.Services.Any(s => s.OwnerId == u.Id))
                .Count();
        }
        public int GetSkillsCount()
        {
            return dbContext.Skills.Count();
        }
        public string GetUserProfilePhoto(string username)
        {
            string userProfilePhoto = null!;
            if (username != null)
            {
                userProfilePhoto = dbContext.Users
                   .FirstOrDefault(u => u.UserName == username)!
                   .ProfilePhoto;
            }
            return userProfilePhoto;
        }
        public async Task<int> UpdateUserProps(SkillBoxUser user)
        {
            var userFromDb = dbContext.Users.SingleOrDefault(u => u.Id == user.Id);
            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.UserName = user.UserName;
            userFromDb.Email = user.Email;
            userFromDb.PhoneNumber = user.PhoneNumber;
            userFromDb.WebsiteName = user.WebsiteName;
            userFromDb.Bio = user.Bio;
            userFromDb.City = user.City;
            userFromDb.Gender = user.Gender;
            dbContext.Users.Update(userFromDb);
            notificationService.CreateNotification(GlobalConstant.UpdateUserPropsNotificationType, user!);
            return await dbContext.SaveChangesAsync();
        }
        public async Task<string> SetProfilePhotoToUser(SkillBoxUser user, string imgURL)
        {
            if (imgURL == null)
            {
                imgURL = GlobalConstant.UserDefaultProfilePhoto;
            }
            var userFromDb = dbContext.Users.SingleOrDefault(u => u.Id == user.Id);
            userFromDb.ProfilePhoto = imgURL;
            dbContext.Users.Update(userFromDb);
            notificationService.CreateNotification(GlobalConstant.UpdateUserPropsNotificationType, user!);
            await dbContext.SaveChangesAsync();
            return userFromDb.ProfilePhoto;
        }
        public void AddSkill(SkillBoxUser user, string skillName, int skillPoints)
        {
            var userFromDb = dbContext.Users
                .SingleOrDefault(u => u.Id == user.Id);
            var skillLevel = GetSkillLevelBySkillPoints(skillPoints);
            if (skillLevel != null)
            {
                userFromDb.Skills.Add(
                    new Skill
                    {
                        Name = skillName,
                        Level = skillLevel
                    });
                dbContext.Users.Update(userFromDb);
                notificationService.CreateNotification(GlobalConstant.UpdateUserPropsNotificationType, user!);
                dbContext.SaveChanges();
            }
        }
        public void AddUserComment(int serviceId, SkillBoxUser user, string content)
        {
            var rnd = new Random();
            var service = offeringService.GetServiceById(serviceId);
            var review = new Review
            {
                Service = service,
                User = user,
                Comment = content,
                RatingStars = rnd.Next(1, 5)
            };
            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();
        }
        private SkillLevel GetSkillLevelBySkillPoints(int skillPoints)
        {
            var skillLevel = dbContext.SkillLevels.FirstOrDefault(sl => sl.BGName == $"{skillPoints}%");
            return skillLevel!;
        }
    }
}