using Data.Models;
using DTOs.OUTPUT;

namespace Contracts
{
    public interface IUserService
    {
        public SkillBoxUser GetUserByUsername(string username);
        public UserDTO GetSkillerDTO(string username);
        public ICollection<UserCardDTO> GetSkillerCardDTOs(int count = 1, int skipCount = 0);
        public ICollection<UserCardDTO> GetAllSkillerAsUserCardDTOs();
        public ICollection<ServiceCardDTO> GetSkillerServicesAsServiceCardDTOs(string username, int count = 1);
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1);
        public ICollection<Skill> GetAllMySkills(string username);
        public ICollection<Review> GetAllMyReviews(string username);
        public bool BlockUser(string username);
        public City GetCityById(int id);
        public int GetUsersCount();
        public int GetSkillersCount();
        public int GetSkillsCount();
        public string GetUserProfilePhoto(string username);
        public Task<int> UpdateUserProps(SkillBoxUser user);
        public Task<string> SetProfilePhotoToUser(SkillBoxUser user, string imgURL);
        public void AddSkill(SkillBoxUser user, string skillName, int skillPoints);
        public void AddUserComment(int serviceId, SkillBoxUser user, string content);
    }
}
