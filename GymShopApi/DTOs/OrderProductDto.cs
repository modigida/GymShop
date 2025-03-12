using GymShopApi.Entities;

namespace GymShopApi.DTOs;
public class OrderProductDto
{
    public int Quantity { get; set; }
    public double CurrentPrice { get; set; }
    public string ProductName { get; set; }
    public int ProductId { get; set; }
}
