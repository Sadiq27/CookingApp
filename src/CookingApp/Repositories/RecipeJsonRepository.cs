using System.Text.Json;
using CookingApp.Models;

namespace CookingApp.Repositories
{
    public class RecipeJsonRepository
    {
        private readonly string filePath = "./Assets/recipes.json";

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            var jsonData = await File.ReadAllTextAsync(filePath);
            using var jsonDoc = JsonDocument.Parse(jsonData);
            var recipes = new List<Recipe>();

            foreach (var element in jsonDoc.RootElement.EnumerateArray())
            {
                var recipe = new Recipe
                {
                    Id = element.TryGetProperty("Id", out JsonElement idElement) && idElement.ValueKind != JsonValueKind.Null ? idElement.GetInt32() : null,
                    Name = element.GetProperty("Name").GetString(),
                    Category = element.GetProperty("Category").GetString(),
                    Instructions = element.GetProperty("Instructions").GetString(),
                    Ingredients = ParseIngredients(element.GetProperty("Ingredients"))
                };
                recipes.Add(recipe);
            }

            return recipes;
        }

        private List<string> ParseIngredients(JsonElement ingredientElement)
        {
            if (ingredientElement.ValueKind == JsonValueKind.Array)
            {
                return ingredientElement.EnumerateArray().Select(x => x.GetString()).ToList();
            }
            else if (ingredientElement.ValueKind == JsonValueKind.String)
            {
                return ingredientElement.GetString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            }
            return new List<string>();
        }

        public async Task CreateNewRecipeAsync(Recipe recipe)
        {
            var recipes = await GetAllRecipesAsync();
            recipe.Id = recipes.Any() ? recipes.Max(r => r.Id) + 1 : 1;
            recipes.Add(recipe);
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonData = JsonSerializer.Serialize(recipes, options);
            await File.WriteAllTextAsync(filePath, jsonData);
        }
    }
}
