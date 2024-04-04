using DTOs.OUTPUT;

namespace Models
{
    public class SingleCategoryViewModel
    {
        public int ServicesCount { get; set; }
        public CategoryDTO Category { get; set; }
        public ICollection<CategoryMiniDTO> CategoryList { get; set; }
        public ICollection<ServiceCardDTO> ServiceCardDTOs { get; set; }
    }
}
