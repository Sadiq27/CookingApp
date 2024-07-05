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
            return await _context.Recipes
                                .Include(r => r.Category)
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(ri => ri.Ingredient)
                                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                            .Include(r => r.Category)
                            .Include(r => r.RecipeIngredients)
                            .ThenInclude(ri => ri.Ingredient)
                            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateNewRecipeAsync(Recipe recipe, IFormFile image)
        {
            var lastRecipe = await _context.Recipes.OrderByDescending(r => r.Id).FirstOrDefaultAsync();

            var extension = new FileInfo(image.FileName).Extension.Substring(1);
            recipe.Image = $"Assets/Images/{lastRecipe.Id +1}.{extension}";

            using (var newFileStream = System.IO.File.Create(recipe.Image))
            {
                await image.CopyToAsync(newFileStream);
            }

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateRecipeAsync(Recipe recipe, IFormFile image)
        {
            var lastRecipe = await _context.Recipes.OrderByDescending(r => r.Id).FirstOrDefaultAsync();

            var extension = new FileInfo(image.FileName).Extension.Substring(1);
            recipe.Image = $"Assets/Images/{lastRecipe.Id +1}.{extension}";
            using (var newFileStream = System.IO.File.Create(recipe.Image))
            {
                await image.CopyToAsync(newFileStream);
            }
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
