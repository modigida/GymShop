using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymShopBlazor.Models;
public class UserCreate
{
    [Required(ErrorMessage = "Förnamn är obligatoriskt")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Efternamn är obligatoriskt")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "E-post är obligatoriskt")]
    [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telefonnummer är obligatoriskt")]
    [Phone(ErrorMessage = "Ogiltigt telefonnummer")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Adress är obligatoriskt")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Lösenord är obligatoriskt")]
    [MinLength(6, ErrorMessage = "Lösenordet måste vara minst 6 tecken")]
    public string Password { get; set; }
    public int RoleId { get; set; } = 1;
}
