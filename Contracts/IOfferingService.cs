using Data.Models;
using DTOs.INPUT;
using DTOs.OUTPUT;

namespace Contracts
{
    public interface IOfferingService
    {
        public SkillBoxService GetServiceById(int id);
        public ServiceDTO GetServiceDTO(int id);
        public ICollection<ServiceCardDTO> GetServiceCardDTOs(int count = 1, int skipCount = 0, int categoryId = 0);
        public ICollection<ServiceMiniDTO> GetAllSkillerServicesByUsername(string username);
        public ICollection<ServiceCardDTO> GetTopServicesAsServiceCardDTOs(int count = 1, int serviceId = 0);
        public ServiceStatus GetServiceStatusById(int id);
        public ICollection<ServiceMiniDTO> GetAllServicesAsServiceMiniDTOs();
        public ICollection<ServiceStatus> GetAllServiceStatuses();
        public ICollection<City> GetAllCities();
        public void CreateService(ServiceInDTO serviceInDTO);
        //public void UpdateService(ServiceInDTO serviceInDTO);
        public bool DeleteService(int id);
        public int GetServicesCount(int categoryId = 0);
        public int GetCompletedServicesCount();
        public int GetReviewsCount();
        public int GetPositiveReiewsCount();
    }
}
