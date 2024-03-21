using DTOs.OUTPUT;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Contracts
{
    public interface ICategoryService
    {
        public ICollection<CategoryCardDTO> GetCategoryCardDTOs(int count = 1, int skipCount = 0);
        public ICollection<CategoryCardDTO> GetAllCategoryCardDTOs();
        public ICollection<CategoryDTO> GetAllCategoryDTOs();
        public ICollection<SelectListItem> GetAllCategoriesAsSelectListItem();
    }
}
