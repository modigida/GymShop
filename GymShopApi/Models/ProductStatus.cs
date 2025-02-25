using System.ComponentModel.DataAnnotations;

namespace GymShopApi.Models;

public class ProductStatus
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty; // Available, Out of Stock, Discontinued, Pre-Order, Limited Edition
}
