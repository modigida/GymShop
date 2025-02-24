namespace GymShopApi.Models;
public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int RoleId { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}
