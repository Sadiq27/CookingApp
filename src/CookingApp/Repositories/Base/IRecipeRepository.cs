using System.Collections.Generic;
using System.Threading.Tasks;
using CookingApp.Models;

namespace CookingApp.Repositories
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        public Task CreateNewRecipeAsync(Recipe recipe, IFormFile image);
        Task UpdateRecipeAsync(Recipe recipe, IFormFile iamge);
        Task DeleteRecipeAsync(int id);
    }
}
