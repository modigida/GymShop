using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GymShopApi.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    public DateTime PurchaseDate { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "OrderStatusId must be a positive integer.")]
    public int OrderStatusId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
}
