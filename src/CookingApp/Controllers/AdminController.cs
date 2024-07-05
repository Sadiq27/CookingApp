using CookingApp.Models;
using CookingApp.Repositories;
using CookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IRecipeRepository _recipeRepository;

    public AdminController(ICategoryService categoryService, IRecipeRepository recipeRepository)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
    }

    [HttpGet("Admin")]
    public IActionResult Dashboard()
    {
        return View();
    }

    // Categories
    [HttpGet("Admin/Categories")]
    public async Task<IActionResult> Categories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    [HttpGet("Admin/CreateCategory")]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost("Admin/CreateCategory")]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.CreateCategoryAsync(category);
            return RedirectToAction(nameof(Categories));
        }
        return View(category);
    }

    [HttpGet("Admin/EditCategory/{id}")]
    public async Task<IActionResult> EditCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost("Admin/EditCategory/{id}")]
    public async Task<IActionResult> EditCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategoryAsync(category);
            return RedirectToAction(nameof(Categories));
        }
        return View(category);
    }

    [HttpGet("Admin/DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost("Admin/DeleteCategory/{id}"), ActionName("DeleteCategory")]
    public async Task<IActionResult> DeleteCategoryConfirmed(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return RedirectToAction(nameof(Categories));
    }

    // Recipes
    [HttpGet("Admin/CreateRecipe")]
    public IActionResult CreateRecipe()
    {
        return View();
    }

    [HttpPost("Admin/CreateRecipe")]
    public async Task<IActionResult> CreateRecipe(Recipe recipe)
    {
        // if (ModelState.IsValid)
        // {
        //     await _recipeRepository.CreateNewRecipeAsync(recipe);
        //     return RedirectToAction("GetAll", "Recipes");
        // }
        return View(recipe);
    }

    [HttpGet("Admin/EditRecipe/{id}")]
    public async Task<IActionResult> EditRecipe(int id)
    {
        var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        return View(recipe);
    }

    [HttpPost("Admin/EditRecipe/{id}")]
    public async Task<IActionResult> EditRecipe(Recipe recipe)
    {
        // if (ModelState.IsValid)
        // {
        //     await _recipeRepository.UpdateRecipeAsync(recipe);
        //     return RedirectToAction("GetAll", "Recipes");
        // }
        return View(recipe);
    }

    [HttpGet("Admin/DeleteRecipe/{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        return View(recipe);
    }

    [HttpPost("Admin/DeleteRecipe/{id}"), ActionName("DeleteRecipe")]
    public async Task<IActionResult> DeleteRecipeConfirmed(int id)
    {
        await _recipeRepository.DeleteRecipeAsync(id);
            return RedirectToAction("GetAll", "Recipes");
    }
}
