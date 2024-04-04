using DTOs.OUTPUT;

namespace Models
{
    public class AllServiceViewModel
    {
        public ICollection<CategoryMiniDTO> CategoryList { get; set; }
        public IEnumerable<ServiceCardDTO> ServiceCardDTOs { get; set; }
    }
}
