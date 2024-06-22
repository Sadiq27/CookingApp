﻿// <auto-generated />
using CookingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CookingApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240621191751_AddUserRoleRelation")]
    partial class AddUserRoleRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                });

            modelBuilder.Entity("CookingApp.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Pasta",
                            Instructions = "Cook spaghetti. Mix eggs and cheese. Cook pancetta. Combine everything.",
                            Name = "Spaghetti Carbonara"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Soup",
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

                    b.Property<string>("Ingredient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ingredient = "Spaghetti",
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Ingredient = "Eggs",
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 3,
                            Ingredient = "Pancetta",
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 4,
                            Ingredient = "Parmesan",
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 5,
                            Ingredient = "Pepper",
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 6,
                            Ingredient = "Tomatoes",
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 7,
                            Ingredient = "Onions",
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 8,
                            Ingredient = "Garlic",
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 9,
                            Ingredient = "Basil",
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 10,
                            Ingredient = "Salt",
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 11,
                            Ingredient = "Pepper",
                            RecipeId = 2
                        });
                });

            modelBuilder.Entity("CookingApp.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("CookingApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "$2a$11$LtGl89P4t53a/MwiKqm0Ru1NkCbX8p1loGslcwftOwGi6Z/wXrPPe",
                            RoleId = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("CookingApp.Models.RecipeIngredient", b =>
                {
                    b.HasOne("CookingApp.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CookingApp.Models.User", b =>
                {
                    b.HasOne("CookingApp.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CookingApp.Models.Recipe", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("CookingApp.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
