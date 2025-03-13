using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService;
public class UserService(HttpClient httpClient)
{
    public async Task<List<UserResponse?>> GetAll()
    {
        var users = await httpClient.GetFromJsonAsync<List<UserResponse>>("https://localhost:7097/api/Users");
        Console.WriteLine(users);
        return users ?? new List<UserResponse>();
    }
    public async Task<UserResponse?> GetUserById(Guid id)
    {
        var user = await httpClient.GetFromJsonAsync<UserResponse>($"https://localhost:7097/api/Users/{id}");
        return user ?? null;
    }

    public async Task<UserResponse?> RegisterUser(UserCreate user)
    {
        var response = await httpClient.PostAsJsonAsync("https://localhost:7097/api/Users/register", user);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }

        return null;
    }
}
