using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService;
public class UserService(HttpClient httpClient)
{
    public async Task<List<UserResponse?>> GetAll()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<UserResponse>>("https://localhost:7097/api/Users/customers")
                ?? new List<UserResponse>();
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<UserResponse>();
        }
    }
    public async Task<UserResponse?> GetUserById(Guid id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<UserResponse>($"https://localhost:7097/api/Users/{id}")
                   ?? null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserResponse?> RegisterUser(UserCreate user)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:7097/api/Users/register", user);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<string?> LoginUser(UserLogin user)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:7097/api/Users/login", user);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return token;

            }

            return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}
