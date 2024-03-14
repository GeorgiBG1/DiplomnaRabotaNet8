using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.INPUT;

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
        public void CreateService(ServiceInDTO serviceInDTO)
        {
            var service = mapper.Map<SkillBoxService>(serviceInDTO);
            dbContext.Services.Add(service);
            dbContext.SaveChanges();
        }
    }
}
