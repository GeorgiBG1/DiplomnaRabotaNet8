using DTOs.OUTPUT;

namespace Contracts
{
    public interface IUserService
    {
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1);
        public int GetSkillersCount();
        public int GetSkillsCount();
    }
}
