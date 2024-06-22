using CookingApp.Models;
using CookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ICategoryService _categoryService;

    public AdminController(ICategoryService categoryService)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    [HttpGet("Admin")]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    // [HttpGet("Admin/CreateCategory")]
    // public IActionResult Create()
    // {
    //     return View();
    // }

    // [HttpPost("Admin/CreateCategory")]
    // public async Task<IActionResult> Create(Category category)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         await _categoryService.CreateCategoryAsync(category);
    //         return RedirectToAction(nameof(Index));
    //     }
    //     return View(category);
    // }

    // [HttpGet("Admin/EditCategory/{id}")]
    // public async Task<IActionResult> Edit(int id)
    // {
    //     var category = await _categoryService.GetCategoryByIdAsync(id);
    //     if (category == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(category);
    // }

    // [HttpPost("Admin/EditCategory/{id}")]
    // public async Task<IActionResult> Edit(Category category)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         await _categoryService.UpdateCategoryAsync(category);
    //         return RedirectToAction(nameof(Index));
    //     }
    //     return View(category);
    // }

    // [HttpGet("Admin/DeleteCategory/{id}")]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     var category = await _categoryService.GetCategoryByIdAsync(id);
    //     if (category == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(category);
    // }

    // [HttpPost("Admin/DeleteCategory/{id}"), ActionName("Delete")]
    // public async Task<IActionResult> DeleteConfirmed(int id)
    // {
    //     await _categoryService.DeleteCategoryAsync(id);
    //     return RedirectToAction(nameof(Index));
    // }
}
