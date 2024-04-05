using DTOs.OUTPUT;

namespace Contracts
{
    public interface IUserService
    {
        public UserDTO GetSkillerDTO(string username);
        public ICollection<UserCardDTO> GetSkillerCardDTOs(int count = 1, int skipCount = 0);
        public ICollection<ServiceCardDTO> GetSkillerServicesAsServiceCardDTOs(string username, int count = 1);
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1);
        public int GetSkillersCount();
        public int GetSkillsCount();
    }
}
