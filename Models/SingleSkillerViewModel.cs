using DTOs.OUTPUT;

namespace Models
{
    public class SingleSkillerViewModel
    {
        public UserDTO Skiller { get; set; }
        public ICollection<CategoryMiniDTO> CategoryList { get; set; }
        public ICollection<ServiceCardDTO> ServiceCardDTOs { get; set; }
    }
}
