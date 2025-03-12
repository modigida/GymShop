using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopBlazor.Models;
public class OrderProduct
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double CurrentPrice { get; set; }

    [JsonIgnore]
    public Order? Order { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
}
