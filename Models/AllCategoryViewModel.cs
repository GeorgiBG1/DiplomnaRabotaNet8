using DTOs.OUTPUT;

namespace Models
{
    public class AllCategoryViewModel
    {
        public ICollection<CategoryMiniDTO> CategoryList { get; set; }
        public IEnumerable<CategoryCardDTO> Categories { get; set; }
    }
}
