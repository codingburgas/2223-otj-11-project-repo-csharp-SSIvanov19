using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CBCanteen.Client.Services.Contracts;
using CBCanteen.Shared.Models.Canteen;

namespace CBCanteen.Client.Services.Implementations;

/// <summary>
/// Meal service.
/// </summary>
internal class MealService : IMealService
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MealService"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    public MealService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task CreateMealAsync(MealIM mealIm)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                name = mealIm.Name,
                unitPrice = mealIm.UnitPrice,
                weight = mealIm.Weight,
                category = mealIm.Category,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PostAsync("api/Meals", jsonContent);
    }

    /// <inheritdoc/>
    public async Task EditMealAsync(string mealId, MealIM mealIm)
    {
        using StringContent jsonContent = new (
            JsonSerializer.Serialize(new
            {
                name = mealIm.Name,
                unitPrice = mealIm.UnitPrice,
                weight = mealIm.Weight,
                category = mealIm.Category,
            }),
            Encoding.UTF8,
            "application/json");

        await this.httpClient.PutAsync($"api/Meals/{mealId}", jsonContent);
    }

    /// <inheritdoc/>
    public async Task<List<MealVM>> GetAllMealsAsync()
    {
        return (await this.httpClient.GetFromJsonAsync<List<MealVM>>("api/Meals")) !;
    }
}
