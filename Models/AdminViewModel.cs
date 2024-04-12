using DTOs.OUTPUT;

namespace Models
{
    public class AdminViewModel
    {
        public IEnumerable<CategoryCardDTO> Categories { get; set; }
        public IEnumerable<ServiceInfoDTO> Services { get; set; }
        public IEnumerable<UserInfoDTO> Skillers { get; set; }
        public int UsersCount { get; set; }
        public int SkillersCount { get; set; }
        public int ReviewsCount { get; set; }
        public int ChatsCount { get; set; }
        public int CompletedServicesCount { get; set; }
        public int SkillsCount { get; set; }
    }
}
