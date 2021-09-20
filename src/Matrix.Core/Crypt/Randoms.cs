using System;
using System.Security.Cryptography;
using System.Text;

namespace Matrix.Crypt
{
    public class Randoms
    {
        public static byte[] GenerateRandom(int lenght)
        {
            var random = new Byte[lenght];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return random;
        }

        public static string GenerateRandomString(int lenght)
        {
            const string content = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var sb = new StringBuilder();

            var rnd = new Random();
            for (int i = 0; i < lenght; i++)
                sb.Append(content[rnd.Next(content.Length)]);

            return sb.ToString();
        }
    }
}
