using DTOs.OUTPUT;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models
{
    public class HomeViewModel
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public ICollection<CategoryCardDTO> CategoryCardDTOs { get; set; }
        public ICollection<UserCardDTO> UserCardDTOs { get; set; }
    }
}
