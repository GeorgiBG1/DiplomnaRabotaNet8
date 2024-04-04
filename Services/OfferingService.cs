using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.INPUT;
using DTOs.OUTPUT;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class OfferingService : IOfferingService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly IMapper mapper;

        public OfferingService(SkillBoxDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public ServiceDTO GetServiceDTO(int id)
        {
            var service = dbContext.Services.Where(s => s.Id == id)
                .Include(s => s.Owner)
                .Include(s => s.City)
                .Include(s => s.Reviews!)
                .ThenInclude(r => r.User)
                .ThenInclude(u => u.City)
                .FirstOrDefault();
            var model = mapper.Map<ServiceDTO>(service);
            return model;
        }
        public ICollection<ServiceCardDTO> GetServiceCardDTOs(int count = 1, int skipCount = 0, int categoryId = 0)
        {
            var services = new List<SkillBoxService>();
            var model = new List<ServiceCardDTO>();
            if (categoryId != 0)
            {
                services = dbContext.Services
                  .Include(s => s.Category)
                  .Where(s => s.CategoryId == categoryId)
                  .OrderByDescending(s => s.Id)
                  .Include(s => s.Owner)
                  .Skip(skipCount)
                  .Take(count).ToList();
                if (services.Count == 0)
                {
                    services = dbContext.Categories
                      .Where(c => c.ParentCategoryId == categoryId)
                      .Include(c => c.Services!)
                      .ThenInclude(s => s.Owner)
                      .SelectMany(c => c.Services!)
                      .OrderByDescending(s => s.Id)
                      .Skip(skipCount)
                      .Take(count).ToList();
                }
            }
            else
            {
                services = dbContext.Services
                  .OrderByDescending(s => s.Id)
                  .Include(s => s.Category)
                  .Include(s => s.Owner)
                  .Skip(skipCount)
                  .Take(count).ToList();
            }
            services.ForEach(s => s.Description = s.Description![..50]);
            model = services.Select(mapper.Map<ServiceCardDTO>).ToList();
            return model;
        }
        public ICollection<ServiceCardDTO> GetTopServicesAsServiceCardDTOs(int count = 1, int serviceId = 0)
        {
            var services = dbContext.Services
                .Include(s => s.Category)
                .Include(s => s.Owner)
                .Include(s => s.Reviews)
                .Where(s => s.Id != serviceId && s.Id != 0)
                .Take(count)
                .OrderByDescending(s => s.Id)
                .ThenByDescending(s => s.Reviews!.Count())
                .ToList();
            var model = services.Select(mapper.Map<ServiceCardDTO>).ToList();
            return model;
        }
        public void CreateService(ServiceInDTO serviceInDTO)
        {
            var service = mapper.Map<SkillBoxService>(serviceInDTO);
            dbContext.Services.Add(service);
            dbContext.SaveChanges();
        }
        public int GetServicesCount(int categoryId = 0)
        {
            if (categoryId != 0)
            {
                var servicesCount = dbContext.Services
                    .Where(s => s.CategoryId == categoryId)
                    .Count();
                if (servicesCount == 0)
                {
                    servicesCount = dbContext.Categories
                        .Where(c => c.ParentCategoryId == categoryId)
                        .Include(c => c.Services)
                        .SelectMany(c => c.Services!)
                        .Count();
                }
                return servicesCount;
            }
            return dbContext.Services.Count();
        }
        public int GetPositiveReiewsCount()
        {
            return dbContext.Reviews.Where(r => r.RatingStars > 2).Count();
        }
    }
}
