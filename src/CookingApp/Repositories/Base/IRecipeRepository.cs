using System.Collections.Generic;
using System.Threading.Tasks;
using CookingApp.Models;

namespace CookingApp.Repositories
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<Recipe> CreateNewRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int id);
    }
}
