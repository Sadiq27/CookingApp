using Microsoft.EntityFrameworkCore;
using CookingApp.Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasKey(r => r.Id);
            modelBuilder.Entity<List<string>>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
