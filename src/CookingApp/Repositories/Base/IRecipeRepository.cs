using System.Collections.Generic;
using System.Threading.Tasks;
using CookingApp.Models;

namespace CookingApp.Repositories
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe> CreateNewRecipeAsync(Recipe recipe);
    }
}
