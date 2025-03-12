using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService;
public class UserService(HttpClient httpClient)
{
    public async Task<List<User?>> GetAll()
    {
        var users = await httpClient.GetFromJsonAsync<List<User>>("https://localhost:7097/api/Users");
        Console.WriteLine(users);
        return users ?? new List<User>();
    }
    public async Task<User?> GetUserById(Guid id)
    {
        var user = await httpClient.GetFromJsonAsync<User>($"https://localhost:7097/api/user/{id}");
        return user ?? null;
    }
}
