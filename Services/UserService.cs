using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStack;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly UserManager<SkillBoxUser> userManager;
        private readonly IMapper mapper;

        public UserService(SkillBoxDbContext dbContext,
            UserManager<SkillBoxUser> userManager,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public SkillBoxUser GetUserByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(u => u.UserName == username)!;
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
                .Include(u => u.Services)
                .ThenInclude(s => s.Reviews)
                .OrderByDescending(u => u.Services.SelectMany(s => s.Reviews!).Count())
                .Skip(skipCount)
                .Take(count).ToList();
            var model = skillers.Select(mapper.Map<UserCardDTO>).ToList();
            return model;
        }
        public ICollection<ServiceCardDTO> GetSkillerServicesAsServiceCardDTOs(string username, int count = 1)
        {
            var services = dbContext.Services
                .Where(s => s.Owner.UserName == username)
                .OrderByDescending(s => s.Id)
                .Include(s => s.Category)
                .Take(count).ToList();
            if (services.Any(s => s.Name.Length > 50))
            {
                services.ForEach(s => s.Name = $"{s.Name![..47]}...");
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
                .Include(u => u.Services)
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
    }
}
