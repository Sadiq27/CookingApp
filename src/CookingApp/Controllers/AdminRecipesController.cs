using CookingApp.Models;
using CookingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminRecipesController : Controller
{
    private readonly IRecipeRepository _recipeRepository;

    public AdminRecipesController(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
    }

    [HttpGet("Admin/Recipes")]
    public async Task<IActionResult> Index()
    {
        var recipes = await _recipeRepository.GetAllRecipesAsync();
        return View(recipes);
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
            await _recipeRepository.CreateNewRecipeAsync(recipe);
            return RedirectToAction("Index");
        }
        return View(recipe);
    }
}
