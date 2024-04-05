using DTOs.OUTPUT;

namespace Models
{
    public class AllSkillerViewModel
    {
        public int SkillersCount { get; set; }
        public ICollection<CategoryMiniDTO> CategoryList { get; set; }
        public IEnumerable<UserCardDTO> Skillers { get; set; }
    }
}
