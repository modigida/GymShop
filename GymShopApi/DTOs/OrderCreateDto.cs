using GymShopApi.Entities;

namespace GymShopApi.DTOs;
public class OrderCreateDto
{
    public int Id { get; set; }
    public UserResponseDto User { get; set; }
    public DateTime PurchaseDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderProductDto> OrderProducts { get; set; }
}
