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
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent.Trim('"');

            }

            return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public async Task<UserResponse> UpdateUser(Guid userId, UserCreate updatedUser)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7097/api/Users/{userId}", updatedUser);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponse>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Fel vid uppdatering: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            throw;
        }
    }
    public async Task<bool> DeleteUser(Guid id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"https://localhost:7097/api/Users/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Fel vid borttagning av användare: {response.StatusCode} - {errorMessage}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett undantag uppstod vid borttagning av användare: {ex.Message}");
            return false;
        }
    }

    public async Task<List<Role>> GetAllRoles()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<Role>>("https://localhost:7097/api/Roles")
                   ?? new List<Role>();
        }
        catch
        {
            return new List<Role>();
        }
    }
}
