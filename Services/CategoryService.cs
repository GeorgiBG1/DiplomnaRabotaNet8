using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SkillBoxDbContext dbContext;
        private readonly IMapper mapper;

        public CategoryService(SkillBoxDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public Category GetCategoryById(int id)
        {
            var category = dbContext.Categories
                .Where(c => !c.IsDeleted)
                .FirstOrDefault(c => c.Id == id);
            return category!;
        }
        public CategoryDTO GetCategoryDTO(int id)
        {
            var category = dbContext.Categories
                .Where(c => c.Id == id)
                .Where(c => !c.IsDeleted)
                .FirstOrDefault();
            var model = mapper.Map<CategoryDTO>(category);
            return model;
        }
        public ICollection<CategoryMiniDTO> GetCategoryMiniDTOs(int count = 1)
        {
            var categories = dbContext.Categories
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.Id)
                .Take(count).ToList();
            var model = categories.Select(mapper.Map<CategoryMiniDTO>).ToList();
            return model;
        }
        public ICollection<CategoryCardDTO> GetCategoryCardDTOs(int count = 1, int skipCount = 0)
        {
            var categories = new List<Category>();
            var model = new List<CategoryCardDTO>();
            if (skipCount != 0)
            {
                categories = dbContext.Categories
                    .Where(c => !c.IsDeleted)
                    .OrderByDescending(s => s.Id)
                    .Include(s => s.Kids)
                    .Include(s => s.Services!.Where(s => !s.IsDeleted))
                    .Skip(skipCount).Take(count).ToList();
                model = categories.Select(mapper.Map<CategoryCardDTO>).ToList();
                return model;
            }
            categories = dbContext.Categories
                .Where(c => !c.IsDeleted)
                .OrderByDescending(s => s.Id)
                .Include(s => s.Kids)
                .Include(s => s.Services!.Where(s => !s.IsDeleted))
                .Take(count).ToList();
            model = categories.Select(mapper.Map<CategoryCardDTO>).ToList();
            return model;
        }
        public ICollection<Category> GetAllCategories() => dbContext.Categories.Where(c => !c.IsDeleted).ToList();
        public ICollection<CategoryCardDTO> GetAllCategoryCardDTOs()
        {
            var categories = new List<Category>();
            var model = new List<CategoryCardDTO>();
            categories = dbContext.Categories
                .Where(c => !c.IsDeleted)
                .OrderByDescending(s => s.Id)
                .Include(s => s.Kids)
                .Include(s => s.Services!.Where(s => !s.IsDeleted))
                .ToList();

            model = categories.Select(mapper.Map<CategoryCardDTO>).ToList();
            return model;
        }
        public ICollection<CategoryDTO> GetAllCategoryDTOs()
        {
            var categories = new List<Category>();
            var model = new List<CategoryDTO>();
            categories = dbContext.Categories
                .Where(c => !c.IsDeleted)
                .OrderByDescending(s => s.Id)
                .Where(s => s.ParentCategoryId == null)
                .Include(s => s.Kids)
                .ToList();

            model = categories.Select(mapper.Map<CategoryDTO>).ToList();
            return model;
        }
        public ICollection<SelectListItem> GetAllCategoriesAsSelectListItem()
        {
            var categories = dbContext.Categories
                .Where(c => !c.IsDeleted)
                .OrderByDescending(s => s.Id)
                .Include(s => s.Kids)
                .ToList();

            var model = categories.Select(mapper.Map<SelectListItem>).ToList();
            return model;
        }
        public ICollection<City> GetAllCities()
        {
            return dbContext.Cities.ToList();
        }
    }
}
