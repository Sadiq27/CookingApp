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
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasKey(r => r.Id);
            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            modelBuilder.Entity<RecipeIngredient>().HasKey(ri => ri.Id);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeIngredients)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<Ingredient>()
                .HasMany(i => i.RecipeIngredients)
                .WithOne(ri => ri.Ingredient)
                .HasForeignKey(ri => ri.IngredientId);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany()
                .HasForeignKey(r => r.CategoryId);


                modelBuilder.Entity<Category>()
                .HasMany(c => c.Recipes)
                .WithOne(r => r.Category)
                .HasForeignKey(r => r.CategoryId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Pasta" },
                new Category { Id = 2, Name = "Soup" }
            );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Name = "Spaghetti Carbonara",
                    Image = "Assets/Images/1.jpg",
                    CategoryId = 1,
                    Instructions = "Cook spaghetti. Mix eggs and cheese. Cook pancetta. Combine everything."
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Tomato Soup",
                    Image = "Assets/Images/2.jpg",
                    CategoryId = 2,
                    Instructions = "Cook onions and garlic. Add tomatoes. Simmer. Blend. Season."
                }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Spaghetti" },
                new Ingredient { Id = 2, Name = "Eggs" },
                new Ingredient { Id = 3, Name = "Pancetta" },
                new Ingredient { Id = 4, Name = "Parmesan" },
                new Ingredient { Id = 5, Name = "Pepper" },
                new Ingredient { Id = 6, Name = "Tomatoes" },
                new Ingredient { Id = 7, Name = "Onions" },
                new Ingredient { Id = 8, Name = "Garlic" },
                new Ingredient { Id = 9, Name = "Basil" },
                new Ingredient { Id = 10, Name = "Salt" },
                new Ingredient { Id = 11, Name = "Pepper" }
            );

            modelBuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient { Id = 1, RecipeId = 1, IngredientId = 1 },
                new RecipeIngredient { Id = 2, RecipeId = 1, IngredientId = 2 },
                new RecipeIngredient { Id = 3, RecipeId = 1, IngredientId = 3 },
                new RecipeIngredient { Id = 4, RecipeId = 1, IngredientId = 4 },
                new RecipeIngredient { Id = 5, RecipeId = 1, IngredientId = 5 },
                new RecipeIngredient { Id = 6, RecipeId = 2, IngredientId = 6 },
                new RecipeIngredient { Id = 7, RecipeId = 2, IngredientId = 7 },
                new RecipeIngredient { Id = 8, RecipeId = 2, IngredientId = 8 },
                new RecipeIngredient { Id = 9, RecipeId = 2, IngredientId = 9 },
                new RecipeIngredient { Id = 10, RecipeId = 2, IngredientId = 10 },
                new RecipeIngredient { Id = 11, RecipeId = 2, IngredientId = 11 }
            );

            var adminUser = new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin"
            };

            modelBuilder.Entity<User>().HasData(adminUser);

            base.OnModelCreating(modelBuilder);
        }
    }
}
