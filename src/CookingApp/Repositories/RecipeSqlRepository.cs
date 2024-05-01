namespace CookingApp.Repositories;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using CookingApp.Models;
using System.Linq;

public class RecipeSqlRepository
{
    private const string connectionString = "Server=localhost;Database=CookingDb;User Id=admin;Password=admin;TrustServerCertificate=True";

public async Task<IEnumerable<Recipe>> GetAllRecipesAsync() {
    using var connection = new SqlConnection(connectionString);
    var recipeData = await connection.QueryAsync<dynamic>("select * from Recipes");

    var recipes = recipeData.Select(rd => new Recipe {
        Id = rd.Id,
        Name = rd.Name,
        Category = rd.Category,
        Ingredients = ((string)rd.Ingredients)?.Split(',')?.ToList() ?? new List<string>(),
        Instructions = rd.Instructions
    });

    return recipes;
}

public async Task<Recipe?> GetRecipeByIdAsync(int id) {
    using var connection = new SqlConnection(connectionString);
    var recipeData = await connection.QueryFirstOrDefaultAsync<dynamic>(
        "select * from Recipes where Id = @Id",
        new { Id = id }
    );

    if (recipeData != null) {
        return new Recipe {
            Id = recipeData.Id,
            Name = recipeData.Name,
            Category = recipeData.Category,
            Ingredients = recipeData.Ingredients?.Split(',').ToList(),
            Instructions = recipeData.Instructions
        };
    }

    return null;
}


    public async Task CreateNewRecipeAsync(Recipe newRecipe) {
        using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(
            @"insert into Recipes (Name, Category, Ingredients, Instructions)
            values (@Name, @Category, @Ingredients, @Instructions)",
            new { 
                newRecipe.Name, 
                newRecipe.Category, 
                Ingredients = string.Join(",", newRecipe.Ingredients),
                newRecipe.Instructions 
            }
        );
    }
}
