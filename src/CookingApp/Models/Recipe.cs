using System.Collections.Generic;

namespace CookingApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Instructions { get; set; }
        public ICollection<RecipeIngredient> Ingredients { get; set; }
    }
}