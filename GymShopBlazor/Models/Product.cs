using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopBlazor.Models;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Category? Category { get; set; }
    public ProductStatus? ProductStatus { get; set; }
    public int? Balance { get; set; }
    public double? Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
