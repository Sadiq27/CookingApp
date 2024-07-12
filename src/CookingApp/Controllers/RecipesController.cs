using Microsoft.AspNetCore.Mvc;
using CookingApp.Models;
using CookingApp.Repositories;

namespace CookingApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> Image(int id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            if (recipe == null || string.IsNullOrEmpty(recipe.Image))
            {
                return NotFound("Recipe or image not found.");
            }
            var fileStream = System.IO.File.Open(recipe.Image!, FileMode.Open);
            return File(fileStream, "image/jpeg");
        }

        [HttpGet("Recipes/GetAll")]
        public async Task<IActionResult> GetAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetAllRecipesAsync();
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
        public async Task<IActionResult> CreateNewRecipe(Recipe recipe, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                await _recipeRepository.CreateNewRecipeAsync(recipe, image);
                return RedirectToAction("GetAllRecipes");
            }
            return View(recipe);
        }


        [HttpGet]
        [Route("[controller]/{recipe.Name}/{id}", Name = "RecipeInfo")]
        public async Task<IActionResult> More(int id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            return View(recipe);
        }
    }
}