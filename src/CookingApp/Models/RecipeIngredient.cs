namespace CookingApp.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; } 
        public string IngredientName { get; set; }
        public Recipe Recipe { get; set; } 
    }
}
