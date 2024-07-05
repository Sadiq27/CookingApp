using CookingApp.Data;
using CookingApp.Models;
using CookingApp.Repositories;
using CookingApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IRecipeRepository _recipeRepository;
    private readonly ApplicationDbContext _context;

    public AdminController(ICategoryService categoryService, IRecipeRepository recipeRepository, ApplicationDbContext context)
    {
        _categoryService = categoryService;
        _context = context ?? throw new ArgumentNullException(nameof(context));
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
    public async Task<IActionResult> CreateCategory()
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
    public async Task<IActionResult> CreateRecipe()
    {
        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.Ingredients = await _context.Ingredients.ToListAsync();
        return View();
    }



    [HttpPost("Admin/CreateRecipe")]
    public async Task<IActionResult> CreateRecipe(Recipe recipe, IFormFile image)
    {
        recipe.RecipeIngredients = recipe.SelectedIngredientIds
                                    .Select(id => new RecipeIngredient { IngredientId = id })
                                    .ToList();

        await _recipeRepository.CreateNewRecipeAsync(recipe, image);

        return RedirectToAction("GetAll", "Recipes");
    }

    [HttpGet("Admin/EditRecipe/{id}")]
    public async Task<IActionResult> EditRecipe(int id)
    {
        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.Ingredients = await _context.Ingredients.ToListAsync();
        var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        return View(recipe);
    }

    [HttpPost("Admin/EditRecipe/{id}")]
    public async Task<IActionResult> EditRecipe([FromForm] Recipe recipe, IFormFile image)
    {
        await _recipeRepository.UpdateRecipeAsync(recipe, image);
        return RedirectToAction("GetAll", "Recipes");
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


    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[controller]/[action]", Name = "AdminManage")]
    public async Task<IActionResult> Access(string email)
    {
        if (email == null)
        {
            return base.View();
        }
        var users = await _context.Users.ToListAsync();
        var user = users?.FirstOrDefault(u => u.Email == email);
        return base.View(user);
    }

    [Authorize(Roles = "Admin,")]
    [HttpPost]
    [Route("[controller]/[action]", Name = "GiveAccess")]
    public async Task<IActionResult> GiveAccess([FromForm] string email, [FromForm] string role)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, $"Пользователь с email: '{email}' не найден");
            return View();
        }

        user.Role = role;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction(controllerName: "Home", actionName: "Index");
    }


}
