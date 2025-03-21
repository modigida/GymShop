using System.Net.Http;
using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService
{
    public class OrderService(HttpClient httpClient, UserService userService)
    {
        public async Task<List<OrderResponse>> GetAll()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<OrderResponse>>("https://localhost:7097/api/Orders")
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
                return await httpClient.GetFromJsonAsync<OrderResponse>($"https://localhost:7097/api/Orders/id/{id}")
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
                return await httpClient.GetFromJsonAsync<List<OrderResponse>>($"https://localhost:7097/api/Orders/email/{email}")
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
                var response = await httpClient.PostAsJsonAsync("https://localhost:7097/api/Orders", order);
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
                return await httpClient.GetFromJsonAsync<List<OrderStatus>>("https://localhost:7097/api/OrderStatuses")
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
                updatedOrder.User = await userService.GetUserById(updatedOrder.User.Id);
                var response = await httpClient.PutAsJsonAsync($"https://localhost:7097/api/Orders/{updatedOrder.Id}", updatedOrder);

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
                var response = await httpClient.DeleteAsync($"https://localhost:7097/api/Orders/{id}");
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

