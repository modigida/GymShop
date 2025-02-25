using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymShopApi.Models;
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    [ForeignKey("ProductStatus")]
    public int ProductStatusId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Balance can´t be negative")]
    public int Balance { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public double Price { get; set; }

    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
}
