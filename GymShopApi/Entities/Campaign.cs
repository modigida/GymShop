using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopApi.Entities;
public class Campaign
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public TimeSpan Duration { get; set; }

    [JsonIgnore]
    public List<CampaignProduct>? CampaignProducts { get; set; }
}
