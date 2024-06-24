using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public Recipe Recipe { get; set; }
    }
}