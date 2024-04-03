using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1)
        {
            var skillers = dbContext.Users
                .Where(u => u.Services.Any())
                .Include(u => u.City)
                .Include(u => u.Skills)
                .Include(u => u.Services)
                .ThenInclude(s => s.Reviews)
                .OrderByDescending(u => u.Services.SelectMany(s => s.Reviews!).Count())
                .Take(count).ToList();
            var model = skillers.Select(mapper.Map<UserCardDTO>).ToList();
            return model;
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
    }
}
