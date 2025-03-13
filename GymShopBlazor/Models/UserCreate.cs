namespace GymShopBlazor.Models;
public class UserCreate
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string Password { get; set; }
    public required int RoleId { get; set; } = 1;
}
