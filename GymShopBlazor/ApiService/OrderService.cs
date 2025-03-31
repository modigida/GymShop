using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using GymShopBlazor.AuthService;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService
{
    public class OrderService(HttpClient httpClient, UserService userService, AuthStateProvider authStateProvider)
    {
        public async Task<List<OrderResponse>> GetAll()
        {
            try
            {
                var tokenObject = await authStateProvider.GetJwtTokenAsync();
                if (!string.IsNullOrEmpty(tokenObject))
                {
                    try
                    {
                        var jsonDov = JsonDocument.Parse(tokenObject);
                        if (jsonDov.RootElement.TryGetProperty("token", out var tokenProperty))
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

                return await httpClient.GetFromJsonAsync<List<OrderResponse>>("api/Orders")
                    ?? new List<OrderResponse>();
            }
            catch
            {
                return new List<OrderResponse>();
            }
        }

        public async Task<OrderResponse> GetById(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<OrderResponse>($"api/Orders/id/{id}")
                       ?? new OrderResponse();
            }
            catch
            {
                return new OrderResponse();
            }
        }
        public async Task<List<OrderResponse>> GetOrdersByUserEmail(string email)
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

                return await httpClient.GetFromJsonAsync<List<OrderResponse>>($"api/Orders/email/{email}")
                       ?? new List<OrderResponse>();
            }
            catch
            {
                return new List<OrderResponse>();
            }
        }

        public async Task<OrderResponse> CreateOrder(OrderCreate order)
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

                var response = await httpClient.PostAsJsonAsync("api/Orders", order);
                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            catch
            {
                return new OrderResponse();
            }
        }

        public async Task<List<OrderStatus>> GetOrderStatuses()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<OrderStatus>>("api/OrderStatuses")
                       ?? new List<OrderStatus>();
            }
            catch
            {
                return new List<OrderStatus>();
            }
        }

        public async Task<OrderResponse> UpdateOrder(OrderCreate updatedOrder)
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

                updatedOrder.User = await userService.GetUserById(updatedOrder.User.Id);
                var response = await httpClient.PutAsJsonAsync($"api/Orders/{updatedOrder.Id}", updatedOrder);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"UpdateOrder failed: {errorMessage}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<OrderResponse>();
            }
            catch
            {
                return new OrderResponse();
            }
        }

        public async Task<bool> DeleteOrder(int id)
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

                var response = await httpClient.DeleteAsync($"api/Orders/{id}");
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
    }
}

