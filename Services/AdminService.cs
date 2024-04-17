using AutoMapper;
using Contracts;
using Data;
using DTOs.OUTPUT;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly IMapper mapper;

        public AdminService(SkillBoxDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public IEnumerable<CategoryCardDTO> GetAllCategoriesWithDetails()
        {
            var categories = dbContext.Categories
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.Id)
                .Include(s => s.Kids)
                .Include(c => c.Services)
                .ToList();
            return categories.Select(mapper.Map<CategoryCardDTO>).ToList();
        }
        public IEnumerable<ServiceInfoDTO> GetTop10ServicesAsServiceInfoDTOs()
        {
            var services = dbContext.Services
                .Where(s => !s.IsDeleted)
                .Include(s => s.City)
                .Include(s => s.ServiceStatus)
                .Take(10)
                .OrderByDescending(s => s.Id)
                .ThenByDescending(s => s.Reviews!.Count())
                .ToList();
            var model = services.Select(mapper.Map<ServiceInfoDTO>).ToList();
            return model;
        }
        public IEnumerable<UserInfoDTO> GetTop10SkillersAsUserInfoDTOs()
        {
            var skillers = dbContext.Users
                .Include(u => u.City)
                .Include(u => u.Services)
                .ThenInclude(s => s.Reviews)
                .OrderByDescending(u => u.Services.SelectMany(s => s.Reviews!).Count())
                .Take(10).ToList();
            return skillers.Select(mapper.Map<UserInfoDTO>).ToList();
        }
    }
}
