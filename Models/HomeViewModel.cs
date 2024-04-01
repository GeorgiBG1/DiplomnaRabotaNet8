using DTOs.OUTPUT;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models
{
    public class HomeViewModel
    {
        public int SkillersCount { get; set; }
        public int ServicesCount { get; set; }
        public int PositiveReviewsCount { get; set; }
        public int SkillsCount { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public ICollection<CategoryCardDTO> CategoryCardDTOs { get; set; }
        public ICollection<UserCardDTO> UserCardDTOs { get; set; }
        public ICollection<ServiceCardDTO> ServiceCardDTOs { get; set; }
    }
}
