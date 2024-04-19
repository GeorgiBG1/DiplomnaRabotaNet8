using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.INPUT;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

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
        public SkillBoxService GetServiceById(int id)
        {
            return dbContext.Services
                .Where(s => !s.IsDeleted)
                .Include(s => s.Owner)
                .FirstOrDefault(s => s.Id == id)!;
        }
        public ServiceDTO GetServiceDTO(int id)
        {
            var service = dbContext.Services
                .Where(s => s.Id == id)
                .Where(s => !s.IsDeleted)
                .Include(s => s.Owner)
                .Include(s => s.City)
                .Include(s => s.Reviews!)
                .ThenInclude(r => r.User)
                .ThenInclude(u => u.City)
                .FirstOrDefault();
            var model = mapper.Map<ServiceDTO>(service);
            return model;
        }
        public ServiceUpdateDTO GetServiceAsServiceUpdateDTO(int id)
        {
            var service = dbContext.Services
                .Where(s => s.Id == id)
                .Where(s => !s.IsDeleted)
                .Include(s => s.Owner)
                .Include(s => s.City)
                .Include(s => s.ServiceStatus)
                .Include(s => s.Reviews!)
                .ThenInclude(r => r.User)
                .ThenInclude(u => u.City)
                .FirstOrDefault();
            var model = mapper.Map<ServiceUpdateDTO>(service);
            return model;

        }
        public ICollection<ServiceCardDTO> GetServiceCardDTOs(int count = 1, int skipCount = 0, int categoryId = 0)
        {
            var services = new List<SkillBoxService>();
            var model = new List<ServiceCardDTO>();
            if (categoryId != 0)
            {
                services = dbContext.Services
                    .Where(s => !s.IsDeleted)
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
                      .Where(s => !s.IsDeleted)
                      .OrderByDescending(s => s.Id)
                      .Skip(skipCount)
                      .Take(count).ToList();
                }
            }
            else
            {
                services = dbContext.Services
                    .Where(s => !s.IsDeleted)
                    .OrderByDescending(s => s.Id)
                    .Include(s => s.Category)
                    .Include(s => s.Owner)
                    .Skip(skipCount)
                    .Take(count).ToList();
            }
            var servicesWithLongNameServices = services.Where(s => s.Name.Length > 50).ToList();
            if (servicesWithLongNameServices.Count != 0)
            {
                servicesWithLongNameServices.ForEach(s => s.Name = $"{s.Name![..47]}...");
                foreach (var service in services.Where(s => s.Name.Length > 50))
                {
                    service.Name = servicesWithLongNameServices.FirstOrDefault(s => s.Id == service.Id)!.Name;
                }
                
            }
            model = services.Select(mapper.Map<ServiceCardDTO>).ToList();
            return model;
        }
        public ICollection<ServiceMiniDTO> GetAllSkillerServicesByUsername(string username)
        {
            var services = new List<SkillBoxService>();
            var model = new List<ServiceMiniDTO>();
            services = dbContext.Services
                .Where(s => !s.IsDeleted)
                .Where(s => s.Owner.UserName == username)
                .OrderByDescending(s => s.Id)
                .Include(s => s.ServiceStatus)
                .Include(s => s.City)
                .Include(s => s.Owner)
                .ThenInclude(o => o.Offerings)
                .ToList();
            var servicesWithLongNameServices = services.Where(s => s.Name.Length > 50).ToList();
            if (servicesWithLongNameServices.Count != 0)
            {
                servicesWithLongNameServices.ForEach(s => s.Name = $"{s.Name![..47]}...");
                foreach (var service in services.Where(s => s.Name.Length > 50))
                {
                    service.Name = servicesWithLongNameServices.FirstOrDefault(s => s.Id == service.Id)!.Name;
                }

            }
            model = services.Select(mapper.Map<ServiceMiniDTO>).ToList();
            return model;
        }
        public ICollection<ServiceCardDTO> GetTopServicesAsServiceCardDTOs(int count = 1, int serviceId = 0)
        {
            var services = dbContext.Services
                .Where(s => !s.IsDeleted)
                .Include(s => s.Category)
                .Include(s => s.Owner)
                .Include(s => s.Reviews)
                .Where(s => s.Id != serviceId && s.Id != 0)
                .Take(count)
                .OrderByDescending(s => s.Id)
                .ThenByDescending(s => s.Reviews!.Count())
                .ToList();
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
        public ServiceStatus GetServiceStatusById(int id)
        {
            return dbContext.ServiceStatuses.FirstOrDefault(ss => ss.Id == id)!;
        }
        public ICollection<ServiceMiniDTO> GetAllServicesAsServiceMiniDTOs()
        {
            var services = dbContext.Services
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.Id)
                .Include(s => s.ServiceStatus)
                .Include(s => s.City)
                .Include(s => s.Owner)
                .ThenInclude(o => o.Offerings)
                .ToList();
            var servicesWithLongNameServices = services.Where(s => s.Name.Length > 50).ToList();
            if (servicesWithLongNameServices.Count != 0)
            {
                servicesWithLongNameServices.ForEach(s => s.Name = $"{s.Name![..47]}...");
                foreach (var service in services.Where(s => s.Name.Length > 50))
                {
                    service.Name = servicesWithLongNameServices.FirstOrDefault(s => s.Id == service.Id)!.Name;
                }

            }
            return services.Select(mapper.Map<ServiceMiniDTO>).ToList();
        }
        public ICollection<ServiceStatus> GetAllServiceStatuses() => dbContext.ServiceStatuses.ToList();
        public ICollection<City> GetAllCities() => dbContext.Cities.ToList();
        public void CreateService(ServiceInDTO serviceInDTO)
        {
            var service = mapper.Map<SkillBoxService>(serviceInDTO);
            dbContext.Services.Add(service);
            dbContext.SaveChanges();
        }
        public void UpdateService(ServiceUpdateDTO serviceUpdateDTO)
        {
            var service = dbContext.Services.FirstOrDefault(s => s.Id == serviceUpdateDTO.Id);
            var updatedService = mapper.Map<SkillBoxService>(serviceUpdateDTO);
            service.Name = updatedService.Name;
            service.Description = updatedService.Description;
            service.PhoneNumber = updatedService.PhoneNumber;
            service.WebsiteName = updatedService.WebsiteName;
            service.Price = updatedService.Price;
            service.UnitPrice = updatedService.UnitPrice;
            service.Discount = updatedService.Discount;
            service.Category = updatedService.Category;
            service.City = updatedService.City;
            service.ServiceStatus = updatedService.ServiceStatus;
            service.MainSkill = updatedService.MainSkill;
            service.Schedule = updatedService.Schedule;
            dbContext.SaveChanges();
        }
        public bool DeleteService(int id)
        {
            var service = dbContext.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                service.IsDeleted = true;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public int GetServicesCount(int categoryId = 0)
        {
            if (categoryId != 0)
            {
                var servicesCount = dbContext.Services
                    .Where(s => !s.IsDeleted)
                    .Where(s => s.CategoryId == categoryId)
                    .Count();
                if (servicesCount == 0)
                {
                    servicesCount = dbContext.Categories
                        .Where(c => c.ParentCategoryId == categoryId)
                        .Include(c => c.Services!.Where(s => !s.IsDeleted))
                        .SelectMany(c => c.Services!)
                        .Count();
                }
                return servicesCount;
            }
            return dbContext.Services.Count();
        }
        public int GetCompletedServicesCount()
        {
            return dbContext.Services.Where(s => s.ServiceStatus.Id == 1).Count();
        }
        public int GetReviewsCount()
        {
            return dbContext.Reviews.Count();
        }
        public int GetPositiveReiewsCount()
        {
            return dbContext.Reviews.Where(r => r.RatingStars > 2).Count();
        }
        
    }
}
