using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Instructions { get; set; }

        [NotMapped]
        public List<int> SelectedIngredientIds { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        
    }
}