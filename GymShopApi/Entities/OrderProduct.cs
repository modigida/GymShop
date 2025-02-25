using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopApi.Entities;
public class OrderProduct
{
    [Key, Column(Order = 0)]
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Key, Column(Order = 1)]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Amount must be at least 1.")]
    public int Amount { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be a positive value.")]
    public double TotalPrice { get; set; }

    [JsonIgnore]
    public Order? Order { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
}