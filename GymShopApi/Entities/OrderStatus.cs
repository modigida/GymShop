using System.ComponentModel.DataAnnotations;

namespace GymShopApi.Entities;
public class OrderStatus
{
    [Key]
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; } = string.Empty; // Pending, Processing, Completed
}
