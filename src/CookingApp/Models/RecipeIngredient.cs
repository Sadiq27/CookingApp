using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingApp.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Required]
        public string Ingredient { get; set; }

        public int RecipeId { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
    }
}