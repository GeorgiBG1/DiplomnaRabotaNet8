using DTOs.INPUT;
using DTOs.OUTPUT;

namespace Contracts
{
    public interface IOfferingService
    {
        public ICollection<ServiceCardDTO> GetServiceCardDTOs(int count = 1, int skipCount = 0);
        public void CreateService(ServiceInDTO serviceInDTO);
    }
}
