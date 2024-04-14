using Data.Models;
using DTOs.OUTPUT;

namespace Contracts
{
    public interface IUserService
    {
        public UserDTO GetSkillerDTO(string username);
        public ICollection<UserCardDTO> GetSkillerCardDTOs(int count = 1, int skipCount = 0);
        public ICollection<ServiceCardDTO> GetSkillerServicesAsServiceCardDTOs(string username, int count = 1);
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1);
        public ICollection<Skill> GetAllMySkills(string username);
        public int GetUsersCount();
        public int GetSkillersCount();
        public int GetSkillsCount();
        public string GetUserProfilePhoto(string username);
    }
}
