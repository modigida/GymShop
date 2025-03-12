using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopBlazor.Models;
public class Order
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public int OrderStatusId { get; set; }
    public double TotalPrice { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    [JsonIgnore]
    public OrderStatus? OrderStatus { get; set; }

    [JsonIgnore]
    public List<OrderProduct>? OrderProducts { get; set; }
}
