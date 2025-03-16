using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopBlazor.Models;
public class OrderProduct
{
    public int Quantity { get; set; }
    public double CurrentPrice { get; set; }
    public string ProductName { get; set; }
    public int ProductId { get; set; }
    [JsonIgnore]
    public string? ImageUrl { get; set; }
}
