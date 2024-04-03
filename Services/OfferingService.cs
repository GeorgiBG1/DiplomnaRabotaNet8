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
        public ICollection<ServiceCardDTO> GetServiceCardDTOs(int count = 1, int skipCount = 0)
        {
            var services = new List<SkillBoxService>();
            var model = new List<ServiceCardDTO>();
            if (skipCount != 0)
            {
                services = dbContext.Services
                    .OrderByDescending(s => s.Id)
                    .Include(s => s.Category)
                    .Skip(skipCount).Take(count).ToList();
                services.ForEach(s => s.Description = s.Description![..50]);
                model = services.Select(mapper.Map<ServiceCardDTO>).ToList();
                return model;
            }
            services = dbContext.Services
                .OrderByDescending(s => s.Id)
                .Include(s => s.Category)
                .Take(count).ToList();
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
                .Where(s => s.Id !=  serviceId && s.Id != 0)
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
        public int GetServicesCount()
        {
            return dbContext.Services.Count();
        }
        public int GetPositiveReiewsCount()
        {
            return dbContext.Reviews.Where(r => r.RatingStars > 2).Count();
        }
    }
}
