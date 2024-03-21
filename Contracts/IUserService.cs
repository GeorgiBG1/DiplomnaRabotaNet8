using DTOs.OUTPUT;

namespace Contracts
{
    public interface IUserService
    {
        public ICollection<UserCardDTO> GetTopSkillersAsUserCardDTOs(int count = 1);
    }
}
