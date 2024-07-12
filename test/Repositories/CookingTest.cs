using Moq;
using Xunit;
using CookingApp.Services;
using CookingApp.Repositories;
using CookingApp.Models;

public class CookingTest
{
    [Fact]
    public async Task GetCategoryByIdAsync_ReturnsCategory()
    {
        
        var mockCategoryRepository = new Mock<ICategoryRepository>();
        var categoryId = 1;
        var expectedCategory = new Category { Id = categoryId, Name = "Soup" };
        
        mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(categoryId))
                              .ReturnsAsync(expectedCategory);

        var categoryService = new CategoryService(mockCategoryRepository.Object);

        
        var result = await categoryService.GetCategoryByIdAsync(categoryId);

        
        Assert.NotNull(result);
        Assert.Equal(expectedCategory.Name, result.Name);
    }
}
