using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GymShopApi.Entities;
public class CampaignProduct
{
    [ForeignKey("Campaign")]

    public int CampaignId { get; set; }

    [ForeignKey("Product")]

    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Percent discount must be at least 1.")]
    public double PercentDiscount { get; set; }

    [JsonIgnore]
    public Campaign? Campaign { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
}
