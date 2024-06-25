using CookingApp.Models;
using CookingApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return _categoryRepository.GetAllCategoriesAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            return _categoryRepository.GetCategoryByIdAsync(id);
        }

        public Task<int> CreateCategoryAsync(Category category)
        {
            return _categoryRepository.CreateCategoryAsync(category);
        }

        public Task<bool> UpdateCategoryAsync(Category category)
        {
            return _categoryRepository.UpdateCategoryAsync(category);
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            return _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
