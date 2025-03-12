using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopBlazor.Models;
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int ProductStatusId { get; set; }

    public int Balance { get; set; }

    public double Price { get; set; }

    public string Description { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    //[JsonIgnore]
    //public Category? Category { get; set; }

    //[JsonIgnore]
    //public ProductStatus? ProductStatus { get; set; }
}
