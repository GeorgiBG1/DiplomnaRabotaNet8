using AutoMapper;
using Contracts;
using Data;
using DTOs.OUTPUT;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(SkillBoxDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1)
        {
            var skillers = dbContext.Users
                .Include(u => u.Skills)
                    .Include(u => u.Services)
                    .ThenInclude(s =>s.Reviews)
                .Take(count).ToList();
            var model = skillers.Select(mapper.Map<UserCardDTO>).ToList();
            return model;
        }
    }
}
