namespace GymShopApi.Models;
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CategoryId { get; set; }
    public int Balance { get; set; }
    public double Price { get; set; }
}
