using AutoMapper;
using CBCanteen.Server.WebHost.Models;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Data.Models.Canteen;
using CBCanteen.Server.Services.Contracts;
using CBCanteen.Server.Services.Implementations;
using CBCanteen.Shared.Models.Canteen.Meal;
using CBCanteen.Shared.Models.ReflectionHelpers;
using Microsoft.EntityFrameworkCore;

namespace CBCanteen.Tests.Services;

public class MealServiceTests
{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    private readonly IMealService _service;

    public MealServiceTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        _context = new ApplicationDBContext(dbContextOptions);
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        _service = new MealService(_context, _mapper);
    }

    [Fact]
    public async Task GetAllMealsAsync_Returns_All_Meals()
    {
        // Arrange
        var expectedMeals = new List<Meal>()
        {
            new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 1", UnitPrice = 1.99, Weight = 150, Category = MealCategories.Appetizer },
            new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 2", UnitPrice = 2.99, Weight = 250, Category = MealCategories.MainDish },
            new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 3", UnitPrice = 3.99, Weight = 350, Category = MealCategories.Dessert }
        };

        await _context.Meals.AddRangeAsync(expectedMeals);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllMealsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMeals.Count, result.Count);

        for (int i = 0; i < expectedMeals.Count; i++)
        {
            Assert.Equal(expectedMeals[i].Name, result[i].Name);
            Assert.Equal(expectedMeals[i].UnitPrice, result[i].UnitPrice);
            Assert.Equal(expectedMeals[i].Weight, result[i].Weight);
            Assert.Equal(expectedMeals[i].Category.GetDescription(), result[i].Category);
        }
    }

    [Fact]
    public async Task GetMealByIdAsync_Returns_Meal_With_Given_Id()
    {
        // Arrange
        var expectedMeal = new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 1", UnitPrice = 1.99, Weight = 150, Category = MealCategories.MainDish };

        await _context.Meals.AddAsync(expectedMeal);
        await _context.SaveChangesAsync();
        var mealId = expectedMeal.Id;

        // Act
        var result = await _service.GetMealByIdAsync(mealId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMeal.Name, result.Name);
        Assert.Equal(expectedMeal.UnitPrice, result.UnitPrice);
        Assert.Equal(expectedMeal.Weight, result.Weight);
        Assert.Equal(expectedMeal.Category.GetDescription(), result.Category);
    }

    [Fact]
    public async Task EditMealAsync_Updates_Meal_With_Given_Id()
    {
        // Arrange
        var mealToEdit = new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 1", UnitPrice = 1.99, Weight = 150, Category = MealCategories.MainDish };

        await _context.Meals.AddAsync(mealToEdit);
        await _context.SaveChangesAsync();

        var expectedName = "Test meal edited";
        var expectedUnitPrice = 2.99;
        var expectedWeight = 200;
        var expectedCategory = MealCategories.Appetizer;
        var newMealInfo = new MealIM { Name = expectedName, UnitPrice = expectedUnitPrice, Weight = expectedWeight, Category = expectedCategory };

        // Act
        await _service.EditMealAsync(mealToEdit.Id, newMealInfo);

        // Assert
        var editedMeal = await _context.Meals.FindAsync(mealToEdit.Id);

        Assert.NotNull(editedMeal);
        Assert.Equal(expectedName, editedMeal.Name);
        Assert.Equal(expectedUnitPrice, editedMeal.UnitPrice);
        Assert.Equal(expectedWeight, editedMeal.Weight);
        Assert.Equal(expectedCategory, editedMeal.Category);
    }

    [Fact]
    public async Task SaveMealAsync_Adds_New_Meal()
    {
        // Arrange
        var expectedMeal = new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 1", UnitPrice = 1.99, Weight = 150, Category = MealCategories.MainDish };

        // Act
        await _service.SaveMealAsync(expectedMeal);

        // Assert
        var addedMeal = await _context.Meals.FindAsync(expectedMeal.Id);

        Assert.NotNull(addedMeal);
        Assert.Equal(expectedMeal.Name, addedMeal.Name);
        Assert.Equal(expectedMeal.UnitPrice, addedMeal.UnitPrice);
        Assert.Equal(expectedMeal.Weight, addedMeal.Weight);
        Assert.Equal(expectedMeal.Category, addedMeal.Category);
    }

    [Fact]
    public async Task SearchForMealsAsync_Returns_Meals_With_Keyword_In_Name()
    {
        // Arrange
        var expectedMeals = new List<Meal>()
        {
            new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 1", UnitPrice = 1.99, Weight = 150, Category = MealCategories.Appetizer },
            new Meal { Id = Guid.NewGuid().ToString(), Name = "Another meal", UnitPrice = 2.99, Weight = 250, Category = MealCategories.Dessert },
            new Meal { Id = Guid.NewGuid().ToString(), Name = "Test meal 2", UnitPrice = 3.99, Weight = 350, Category = MealCategories.MainDish }
        };

        await _context.Meals.AddRangeAsync(expectedMeals);
        await _context.SaveChangesAsync();

        var keyword = "Test";

        // Act
        var result = await _service.SearchForMealsAsync(keyword);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMeals.Count(meal => meal.Name.Contains(keyword)), result.Count);

        foreach (var meal in result)
        {
            Assert.Contains(keyword, meal.Name);
        }
    }
}
