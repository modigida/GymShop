using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopApi.Models;
public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(50)] 
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(50)] 
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "RoleId must be a positive integer.")]
    public int RoleId { get; set; }

    [Required]
    [MaxLength(150)]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string PasswordSalt { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;

    [JsonIgnore]
    public Role? Role { get; set; }
}
