using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace BL.Helpers
{
    public static class HashHelper
    {
        public static byte[] GenerateSalt()
        {
            const int SaltLength = 64;
            byte[] salt = new byte[SaltLength];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }

        public static string GenerateSha256Hash(string str, byte[] salt)
        {
            byte[] strBytes = Convert.FromBase64String(str);
            byte[] saltedPassword = strBytes.Concat(salt).ToArray();
            using var hash = SHA256.Create();

            return Convert.ToBase64String(hash.ComputeHash(saltedPassword));
        }

    }
}
