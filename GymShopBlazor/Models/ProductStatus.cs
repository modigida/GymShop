using System.ComponentModel.DataAnnotations;

namespace GymShopBlazor.Models;
public class ProductStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
