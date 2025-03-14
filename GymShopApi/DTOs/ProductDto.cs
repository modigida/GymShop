using GymShopApi.Entities;

namespace GymShopApi.DTOs;
public class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Category? Category { get; set; }
    public ProductStatus? ProductStatus { get; set; }
    public int Balance { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
