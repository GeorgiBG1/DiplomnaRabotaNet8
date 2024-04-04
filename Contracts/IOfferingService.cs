using DTOs.INPUT;
using DTOs.OUTPUT;

namespace Contracts
{
    public interface IOfferingService
    {
        public ServiceDTO GetServiceDTO(int id);
        public ICollection<ServiceCardDTO> GetServiceCardDTOs(int count = 1, int skipCount = 0, int categoryId = 0);
        public void CreateService(ServiceInDTO serviceInDTO);
        public ICollection<ServiceCardDTO> GetTopServicesAsServiceCardDTOs(int count = 1, int serviceId = 0);
        public int GetServicesCount(int categoryId = 0);
        public int GetPositiveReiewsCount();
    }
}
