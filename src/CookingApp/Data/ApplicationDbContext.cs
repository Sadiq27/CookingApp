using CookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasKey(r => r.Id);
            modelBuilder.Entity<RecipeIngredient>().HasKey(ri => ri.Id);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Name = "Spaghetti Carbonara",
                    Category = "Pasta",
                    Instructions = "Cook spaghetti. Mix eggs and cheese. Cook pancetta. Combine everything."
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Tomato Soup",
                    Category = "Soup",
                    Instructions = "Cook onions and garlic. Add tomatoes. Simmer. Blend. Season."
                }
            );

            modelBuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient { Id = 1, Ingredient = "Spaghetti", RecipeId = 1 },
                new RecipeIngredient { Id = 2, Ingredient = "Eggs", RecipeId = 1 },
                new RecipeIngredient { Id = 3, Ingredient = "Pancetta", RecipeId = 1 },
                new RecipeIngredient { Id = 4, Ingredient = "Parmesan", RecipeId = 1 },
                new RecipeIngredient { Id = 5, Ingredient = "Pepper", RecipeId = 1 },
                new RecipeIngredient { Id = 6, Ingredient = "Tomatoes", RecipeId = 2 },
                new RecipeIngredient { Id = 7, Ingredient = "Onions", RecipeId = 2 },
                new RecipeIngredient { Id = 8, Ingredient = "Garlic", RecipeId = 2 },
                new RecipeIngredient { Id = 9, Ingredient = "Basil", RecipeId = 2 },
                new RecipeIngredient { Id = 10, Ingredient = "Salt", RecipeId = 2 },
                new RecipeIngredient { Id = 11, Ingredient = "Pepper", RecipeId = 2 }
            );

            var adminUser = new User
            {
                Id = 1,
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
            };

            modelBuilder.Entity<User>().HasData(adminUser);

            base.OnModelCreating(modelBuilder);
        }
    }
}
