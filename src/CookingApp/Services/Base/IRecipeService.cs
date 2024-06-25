using CookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> GetAllRecipesAsync();
    Task<Recipe> GetRecipeByIdAsync(int id);
    Task CreateRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(int id);
}
