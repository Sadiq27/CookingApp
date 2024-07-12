﻿// <auto-generated />
using CookingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CookingApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CookingApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pasta"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Soup"
                        });
                });

            modelBuilder.Entity("CookingApp.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Spaghetti"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Eggs"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Pancetta"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Parmesan"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Pepper"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Tomatoes"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Onions"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Garlic"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Basil"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Salt"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Pepper"
                        });
                });

            modelBuilder.Entity("CookingApp.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Image = "Assets/Images/1.jpg",
                            Instructions = "Cook spaghetti. Mix eggs and cheese. Cook pancetta. Combine everything.",
                            Name = "Spaghetti Carbonara"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Image = "Assets/Images/2.jpg",
                            Instructions = "Cook onions and garlic. Add tomatoes. Simmer. Blend. Season.",
                            Name = "Tomato Soup"
                        });
                });

            modelBuilder.Entity("CookingApp.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IngredientId = 1,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 2,
                            IngredientId = 2,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 3,
                            IngredientId = 3,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 4,
                            IngredientId = 4,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 5,
                            IngredientId = 5,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 6,
                            IngredientId = 6,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 7,
                            IngredientId = 7,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 8,
                            IngredientId = 8,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 9,
                            IngredientId = 9,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 10,
                            IngredientId = 10,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 11,
                            IngredientId = 11,
                            RecipeId = 2
                        });
                });

            modelBuilder.Entity("CookingApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            Password = "$2a$11$pr4poAW2aBv63Iq6.om/i.e1Cu0kHgbtHq1pzYaqPEPqJerpoz2I2",
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("CookingApp.Models.Recipe", b =>
                {
                    b.HasOne("CookingApp.Models.Category", "Category")
                        .WithMany("Recipes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CookingApp.Models.RecipeIngredient", b =>
                {
                    b.HasOne("CookingApp.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CookingApp.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CookingApp.Models.Category", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("CookingApp.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("CookingApp.Models.Recipe", b =>
                {
                    b.Navigation("RecipeIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
