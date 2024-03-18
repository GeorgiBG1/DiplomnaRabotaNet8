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
        public void CreateService(ServiceInDTO serviceInDTO)
        {
            var service = mapper.Map<SkillBoxService>(serviceInDTO);
            dbContext.Services.Add(service);
            dbContext.SaveChanges();
        }
    }
}
