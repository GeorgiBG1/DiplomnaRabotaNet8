using AutoMapper;
using Contracts;
using Data;
using Data.Models;
using DTOs.OUTPUT;
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
        public ICollection<CategoryCardDTO> GetCategoryCardDTOs(int count = 1, int skipCount = 0)
        {
            var categories = new List<Category>();
            var model = new List<CategoryCardDTO>();
            if (skipCount != 0)
            {
                categories = dbContext.Categories
                    .OrderByDescending(s => s.Id)
                    .Include(s => s.Kids)
                    .Include(s => s.Services)
                    .Skip(skipCount).Take(count).ToList();
                model = categories.Select(mapper.Map<CategoryCardDTO>).ToList();
                return model;
            }
            categories = dbContext.Categories
                    .OrderByDescending(s => s.Id)
                    .Include(s => s.Kids)
                    .Include(s => s.Services)
                .Take(count).ToList();
            model = categories.Select(mapper.Map<CategoryCardDTO>).ToList();
            return model;
        }
        public ICollection<CategoryCardDTO> GetAllCategoryCardDTOs()
        {
            var categories = new List<Category>();
            var model = new List<CategoryCardDTO>();
            categories = dbContext.Categories
                    .OrderByDescending(s => s.Id)
                    .Include(s => s.Kids)
                    .Include(s => s.Services)
                    .ToList();

            model = categories.Select(mapper.Map<CategoryCardDTO>).ToList();
            return model;
        }
        public ICollection<CategoryDTO> GetAllCategoryDTOs()
        {
            var categories = new List<Category>();
            var model = new List<CategoryDTO>();
            categories = dbContext.Categories
                    .OrderByDescending(s => s.Id)
                    .Include(s => s.ParentCategory)
                    .Include(s => s.Kids)
                    .Include(s => s.Services)
                    .ToList();

            model = categories.Select(mapper.Map<CategoryDTO>).ToList();
            return model;
        }
    }
}
