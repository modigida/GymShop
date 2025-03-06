using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopApi.Entities;
public class OrderProduct
{
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public double CurrentPrice { get; set; }

    [JsonIgnore]
    public Order? Order { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
}