using Data.Models;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Contracts
{
    public interface ICategoryService
    {
        public Category GetCategoryById(int id);
        public CategoryDTO GetCategoryDTO(int id);
        public ICollection<CategoryMiniDTO> GetCategoryMiniDTOs(int count = 1);
        public ICollection<CategoryCardDTO> GetCategoryCardDTOs(int count = 1, int skipCount = 0);
        public ICollection<Category> GetAllCategories();
        public ICollection<CategoryCardDTO> GetAllCategoryCardDTOs();
        public ICollection<CategoryDTO> GetAllCategoryDTOs();
        public ICollection<SelectListItem> GetAllCategoriesAsSelectListItem();
        public ICollection<City> GetAllCities();
    }
}
