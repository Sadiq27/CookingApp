using System.ComponentModel.DataAnnotations;

namespace CookingApp.DTOs
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}