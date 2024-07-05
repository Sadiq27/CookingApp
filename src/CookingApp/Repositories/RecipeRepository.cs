using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingApp.Data;
using CookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingApp.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Recipe> CreateNewRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var existingRecipe = await _context.Recipes.FindAsync(recipe.Id);
            if (existingRecipe != null)
            {
                _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
