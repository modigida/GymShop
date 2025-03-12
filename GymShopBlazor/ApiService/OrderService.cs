using System.Net.Http.Json;
using GymShopBlazor.Models;

namespace GymShopBlazor.ApiService
{
    public class OrderService(HttpClient httpClient)
    {
        public async Task<List<Order>> GetAll()
        {
            var orders = await httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7097/api/Orders");
            var orderProducts = await httpClient.GetFromJsonAsync<List<OrderProduct>>("https://localhost:7097/api/OrderProducts");

            foreach (var order in orders)
            {
                order.OrderProducts = orderProducts.Where(op => op.OrderId == order.Id).ToList();
            }

            return orders;
        }
    }
}
