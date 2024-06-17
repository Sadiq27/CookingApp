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

        public async Task<Recipe> CreateNewRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }
    }
}
