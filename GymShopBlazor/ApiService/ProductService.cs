using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService;

public class ProductService(HttpClient httpClient)
{
    public async Task<List<Product?>> GetAll()
    {
        var products = await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7097/api/Products");
        return products ?? new List<Product>();
    }

    public async Task<Product?> GetUserById(int id)
    {
        var product = await httpClient.GetFromJsonAsync<Product>($"https://localhost:7097/api/Products/{id}");
        return product ?? null;
    }

    public async Task<List<Product?>> GetByCategory(Category category)
    {
        var products = await httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:7097/api/Products/category/{category.Id}");
        return products ?? new List<Product>();
    }

    public async Task<List<Category>> GetCategories()
    {
        var categories = await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7097/api/Categories");
        return categories ?? new List<Category>();
    }
}
