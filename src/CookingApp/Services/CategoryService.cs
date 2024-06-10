using System.Collections.Generic;
using System.Threading.Tasks;
using CookingApp.Models;
using CookingApp.Repositories;

namespace CookingApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return categoryRepository.GetAllCategoriesAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            return categoryRepository.GetCategoryByIdAsync(id);
        }

        public Task<int> CreateCategoryAsync(Category category)
        {
            return categoryRepository.CreateCategoryAsync(category);
        }

        public Task<bool> UpdateCategoryAsync(Category category)
        {
            return categoryRepository.UpdateCategoryAsync(category);
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            return categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
