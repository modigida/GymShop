using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace GymShopApi.Hasher;
public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public static string ExtractSalt(string hashedPassword)
    {
        var parts = hashedPassword.Split('$');
        return "$" + string.Join("$", parts.Take(3));
    }
}
