namespace GymShopBlazor.Models;
public class UserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public Role Role { get; set; }
    public override string ToString()
    {
        return $"{LastName.ToUpper()}, {FirstName}";
    }
}
