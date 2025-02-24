namespace GymShopApi.Models;
public class OrderProduct
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }
}