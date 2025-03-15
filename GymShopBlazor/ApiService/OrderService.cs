using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService
{
    public class OrderService(HttpClient httpClient)
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
    }
}
