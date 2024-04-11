using CookingApp.Attributes.Http;
using CookingApp.Controllers.Base;
using CookingApp.Models;
using CookingApp.Repositories;
using CookingApp.Extensions;
using System.Web;

public class RecipesController : ControllerBase
{
    private readonly RecipeSqlRepository recipeSqlRepository;

    public RecipesController()
    {
        this.recipeSqlRepository = new RecipeSqlRepository();
    }

    // GET: "/Recipes/GetAll"
    [HttpGet(ActionName = "GetAll")]
    public async Task GetAllRecipesAsync()
    {
        var recipes = await this.recipeSqlRepository.GetAllRecipesAsync();

        if (recipes is not null && recipes.Any())
        {
            var html = "<a class=\"btn\" href=\"/Recipes/Create\">Create new Recipe</a><br>" + recipes.AsHtml();
            await base.LayoutAsync(html);
        }
        else
        {
            //await new ErrorController().NotFound(nameof(recipes));
        }
    }

    // GET: "/Recipes/Create"
    [HttpGet(ActionName = "Create")]
    public async Task ShowRecipeCreateForm()
    {
        await base.WriteViewAsync("recipecreate");
    }

    // POST: "/Recipes/Create"
    [HttpPost(ActionName = "Create")]
    public async Task CreateNewRecipe()
    {
        string postData;
        using (var reader = new StreamReader(Request.InputStream, Request.ContentEncoding))
        {
            postData = await reader.ReadToEndAsync();
        }

        // Парсим данные формы
        var parsedData = HttpUtility.ParseQueryString(postData);

        string name = parsedData["name"];
        string category = parsedData["category"];
        string ingredients = parsedData["ingredients"];
        string instructions = parsedData["instructions"];

        await recipeSqlRepository.CreateNewRecipeAsync(new Recipe
        {
            Name = name,
            Category = category,
            Ingredients = ingredients.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
            Instructions = instructions
        });

        Response.StatusCode = 201;
    }
}
