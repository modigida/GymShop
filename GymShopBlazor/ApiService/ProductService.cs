using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using GymShopBlazor.AuthService;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService;

public class ProductService(HttpClient httpClient, AuthStateProvider authStateProvider)
{
    public async Task<List<Product?>> GetAll()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<Product>>("api/Products")
                   ?? new List<Product?>();
        }
        catch
        {
            return new List<Product?>();
        }
    }

    public async Task<Product?> GetById(int id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<Product?>($"api/Products/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Product?>> GetByCategory(Category category)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<Product>>(
                       $"api/Products/category/{category.Id}")
                   ?? new List<Product?>();
        }
        catch
        {
            return new List<Product?>();
        }
    }

    public async Task<Product> CreateProduct(Product product)
    {
        try
        {
            var tokenObject = await authStateProvider.GetJwtTokenAsync();
            if (!string.IsNullOrEmpty(tokenObject))
            {
                try
                {
                    var jsonDoc = JsonDocument.Parse(tokenObject);
                    if (jsonDoc.RootElement.TryGetProperty("token", out var tokenProperty))
                    {
                        var token = tokenProperty.ToString();
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            var response = await httpClient.PostAsJsonAsync("api/Products", product);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"CreateOrder failed: {errorMessage}");
                return new Product();
            }
            return await response.Content.ReadFromJsonAsync<Product>();
        }
        catch
        {
            return new Product();
        }
    }
    public async Task<Product> UpdateProduct(Product product)
    {
        try
        {
            var tokenObject = await authStateProvider.GetJwtTokenAsync();
            if (!string.IsNullOrEmpty(tokenObject))
            {
                try
                {
                    var jsonDoc = JsonDocument.Parse(tokenObject);
                    if (jsonDoc.RootElement.TryGetProperty("token", out var tokenProperty))
                    {
                        var token = tokenProperty.ToString();
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            var response = await httpClient.PutAsJsonAsync($"api/Products/{product.Id}", product);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"UpdateOrder failed: {errorMessage}");
                return null;
            }
            return await response.Content.ReadFromJsonAsync<Product>();
        }
        catch
        {
            return new Product();
        }
    }

    public async Task<bool> DeleteProduct(int id)
    {
        try
        {
            var tokenObject = await authStateProvider.GetJwtTokenAsync();
            if (!string.IsNullOrEmpty(tokenObject))
            {
                try
                {
                    var jsonDoc = JsonDocument.Parse(tokenObject);
                    if (jsonDoc.RootElement.TryGetProperty("token", out var tokenProperty))
                    {
                        var token = tokenProperty.ToString();
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            var response = await httpClient.DeleteAsync($"api/Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Fel vid borttagning: {response.StatusCode} - {errorMessage}");
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Category>> GetCategories()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<Category>>("api/Categories")
                   ?? new List<Category>();
        }
        catch
        {
            return new List<Category>();
        }
    }

    public async Task<List<ProductStatus>> GetProductStatuses()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<ProductStatus>>("api/ProductStatuses")
                   ?? new List<ProductStatus>();
        }
        catch
        {
            return new List<ProductStatus>();
        }
    }
}
