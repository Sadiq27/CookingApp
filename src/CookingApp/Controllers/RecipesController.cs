using Microsoft.AspNetCore.Mvc;
using CookingApp.Models;
using CookingApp.Repositories;

namespace CookingApp.Controllers;

public class RecipesController : Controller
{
    private readonly RecipeJsonRepository recipeRepository;

    public RecipesController()
    {
        recipeRepository = new RecipeJsonRepository();
    }

    [HttpGet("Recipes/GetAll")]
    public async Task<IActionResult> GetAllRecipesAsync()
    {
        var recipes = await recipeRepository.GetAllRecipesAsync();
        if (recipes != null && recipes.Any())
        {
            return View("GetAllRecipes", recipes);
        }
        else
        {
            return NotFound("No recipes found");
        }
    }

    [HttpGet("Recipes/Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Recipes/Create")]
    public async Task<IActionResult> CreateNewRecipe(Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            await recipeRepository.CreateNewRecipeAsync(recipe);
            return RedirectToAction("GetAllRecipes");
        }
        return View(recipe);
    }
}
