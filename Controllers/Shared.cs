using System.Security.Cryptography;
using System.Text;

namespace Chat.Controllers;

public static class Shared
{
    public static String GetRandomString(int length)
    {
        if (length < 1) return String.Empty;

        byte[] randomBytes = new byte[(Int32)length * 3 / 4];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }

}