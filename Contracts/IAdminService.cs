using DTOs.OUTPUT;

namespace Contracts
{
    public interface IAdminService
    {
        public IEnumerable<CategoryCardDTO> GetAllCategoriesWithDetails();
        public IEnumerable<ServiceInfoDTO> GetTop10ServicesAsServiceInfoDTOs();
        public IEnumerable<UserInfoDTO> GetTop10SkillersAsUserInfoDTOs();
        public ICollection<AdminMiniDTO> GetAllAdminContacts();
    }
}
