namespace GymShopApi.Models;
public class Order
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int StatusId { get; set; }
}
