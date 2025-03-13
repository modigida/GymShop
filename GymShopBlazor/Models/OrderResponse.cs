using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopBlazor.Models;
public class OrderResponse
{
    public int Id { get; set; }
    public UserResponse User { get; set; }
    public DateTime PurchaseDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public double TotalPrice { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}
