using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingApp.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}