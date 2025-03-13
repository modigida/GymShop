using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService
{
    public class OrderService(HttpClient httpClient)
    {
        public async Task<List<OrderResponse>> GetAll()
        {
            var orders = await httpClient.GetFromJsonAsync<List<OrderResponse>>("https://localhost:7097/api/Orders");

            return orders;
        }
    }
}
