using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Matrix.Core.Crypt
{
    /// <summary>
    /// Summary description for Hash.
    /// </summary>
    public class Hash
    {
        public static HashAlgorithms HashAlgorithmsFromName(string name)
        {
            if (name == null)
                return HashAlgorithms.Unknown;

            return
                Enum.GetValues<HashAlgorithms>()
                    .Cast<HashAlgorithms>()
                    .FirstOrDefault(hash => hash.ToName().ToLower().Equals(name.ToLower()));
        }

        public static HashAlgorithm GetHashAlgorithm(HashAlgorithms hash)
        {
            switch (hash)
            {
                case HashAlgorithms.Unknown:
                    return null;
                case HashAlgorithms.Sha1:
                    return SHA1.Create();
                case HashAlgorithms.Sha256:
                    return SHA256.Create();
                case HashAlgorithms.Sha384:
                    return SHA384.Create();
                case HashAlgorithms.Sha512:
                    return SHA512.Create();
                case HashAlgorithms.Md5:
                    return MD5.Create();
            }
            return null;
        }

        /// <summary>
        /// creates a Sha1 hash with Hex output
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string Sha1HashHex(string pass)
        {
            var hash = Sha1HashBytes(pass);
            return HexToString(hash);
        }

        public static string Sha1HashHex(byte[] pass)
        {
            return HexToString(Sha1HashBytes(pass));
        }

        public static byte[] Sha1HashBytes(string pass)
        {
            var bytes = Encoding.UTF8.GetBytes(pass);
            return Sha1HashBytes(bytes);
        }

        public static byte[] Sha1HashBytes(byte[] pass)
        {
            return SHA1.Create().ComputeHash(pass);
        }

        public static string Sha256HashHex(string pass)
        {
            var hash = Sha256HashBytes(pass);
            return HexToString(hash);
        }

        public static string Sha256HashHex(byte[] pass)
        {
            return HexToString(Sha256HashBytes(pass));
        }

        public static byte[] Sha256HashBytes(string pass)
        {
            var bytes = Encoding.UTF8.GetBytes(pass);
            return Sha256HashBytes(bytes);
        }

        public static byte[] Sha256HashBytes(byte[] pass)
        {
            return SHA256.Create().ComputeHash(pass);
        }

        public static byte[] Md5HashBytes(byte[] pass)
        {
            return MD5.Create().ComputeHash(pass);
        }

        public static byte[] Md5HashBytes(string pass)
        {
            var bytes = Encoding.UTF8.GetBytes(pass);
            return Md5HashBytes(bytes);
        }

        public static string Md5HashHex(string pass)
        {
            var hash = Md5HashBytes(pass);
            return HexToString(hash);
        }

        public static string HMACSHA256HashHex(string key, string data)
        {
            return HexToString(HMACSHA256(key, data));
        }


        public static byte[] HMACSHA256(byte[] key, byte[] data)
        {
            using (var hmacsha256 = new HMACSHA256(key))
            {
                byte[] bytes = hmacsha256.ComputeHash(data);
                return bytes;
            }
        }

        public static byte[] HMACSHA256(string key, byte[] data)
        {
            return HMACSHA256(Encoding.UTF8.GetBytes(key), data);
        }

        public static byte[] HMACSHA256(byte[] key, string data)
        {
            return HMACSHA256(key, Encoding.UTF8.GetBytes(data));
        }

        public static byte[] HMACSHA256(string key, string data)
        {
            return HMACSHA256(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(data));
        }

        public static string HMACHashHex(string key, string data)
        {
            return HexToString(HMAC(key, data));
        }

        public static byte[] HMAC(byte[] key, byte[] data)
        {
            return new System.Security.Cryptography.HMACSHA1(key).ComputeHash(data);
        }

        public static byte[] HMAC(string key, byte[] data)
        {
            return HMAC(Encoding.UTF8.GetBytes(key), data);
        }

        public static byte[] HMAC(byte[] key, string data)
        {
            return HMAC(key, Encoding.UTF8.GetBytes(data));
        }

        public static byte[] HMAC(string key, string data)
        {
            return HMAC(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// Converts all bytes in the Array to a string representation.
        /// </summary>
        /// <param name="buf"></param>
        /// <returns>string representation</returns>
        public static string HexToString(byte[] buf)
        {
            var sb = new StringBuilder();
            foreach (byte b in buf)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}