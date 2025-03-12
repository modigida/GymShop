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
        var product = await httpClient.GetFromJsonAsync<Product>($"api/Products/{id}");
        return product ?? null;
    }
}
