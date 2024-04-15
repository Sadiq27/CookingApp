namespace CookingApp.Controllers;
using CookingApp.Attributes.Http;
using CookingApp.Controllers.Base;
using CookingApp.Extensions;
using CookingApp.Repositories;

public class FilterController : ControllerBase
{
   private readonly RecipeSqlRepository recipeSqlRepository;

    public FilterController()
    {
        this.recipeSqlRepository = new RecipeSqlRepository();
    }

    // GET: "/Filter/ByIngredients"
    [HttpGet(ActionName = "ByIngredients")]
    public async Task GetRecipesByIngredientsAsync(string ingredients)
    {
        Console.WriteLine($"Received ingredients: {ingredients}");
        
        if (string.IsNullOrEmpty(ingredients))
        {
            await WriteViewAsync("filter");
            return;
        }

        var ingredientList = ingredients.Split(',').Select(ing => ing.Trim().ToLower()).ToList();
        var allRecipes = await this.recipeSqlRepository.GetAllRecipesAsync();
        Console.WriteLine($"Total recipes fetched: {allRecipes.Count()}");

        var filteredRecipes = allRecipes.Where(recipe => 
            ingredientList.All(input => 
                recipe.Ingredients.Any(ing => 
                    ing.ToLower().Contains(input)
                )
            )
        ).ToList();

        Console.WriteLine($"Filtered recipes count: {filteredRecipes.Count}");
        if (filteredRecipes.Any())
        {
            var html = "<div class='recipes-list'>" + filteredRecipes.AsHtml() + "</div>";
            await LayoutAsync(html);
        }
        else
        {
            Console.WriteLine("No recipes found with the specified ingredients.");
            await LayoutAsync("<p>No recipes found with the specified ingredients.</p>");
        }
    }
}
