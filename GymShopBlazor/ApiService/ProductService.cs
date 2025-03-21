using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService;

public class ProductService(HttpClient httpClient)
{
    public async Task<List<Product?>> GetAll()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7097/api/Products")
                   ?? new List<Product?>();
        }
        catch
        {
            return new List<Product?>();
        }
    }

    public async Task<Product?> GetUserById(int id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<Product>($"https://localhost:7097/api/Products/{id}")
                   ?? null;
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
                       $"https://localhost:7097/api/Products/category/{category.Id}")
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
            var response = await httpClient.PostAsJsonAsync("https://localhost:7097/api/Products", product);
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
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7097/api/Products/{product.Id}", product);

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
            var response = await httpClient.DeleteAsync($"https://localhost:7097/api/Products/{id}");
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
            return await httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7097/api/Categories")
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
            return await httpClient.GetFromJsonAsync<List<ProductStatus>>("https://localhost:7097/api/ProductStatuses")
                   ?? new List<ProductStatus>();
        }
        catch
        {
            return new List<ProductStatus>();
        }
    }
}
