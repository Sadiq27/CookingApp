using System.ComponentModel.DataAnnotations.Schema;

namespace CookingApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        
        [NotMapped]
        public List<string> Ingredients { get; set; }
        
        public string Instructions { get; set; }
    }

}
