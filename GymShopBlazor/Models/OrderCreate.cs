namespace GymShopBlazor.Models;
public class OrderCreate
{
    public int Id { get; set; }
    public UserResponse User { get; set; }
    public DateTime PurchaseDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}
