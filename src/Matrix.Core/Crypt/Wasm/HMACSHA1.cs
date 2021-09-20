namespace Matrix.Crypt.Wasm
{
    using System.Security.Cryptography;

    internal class HMACSHA1 : HMAC
    {
        public HMACSHA1(byte[] key)
            : this(key, false)
        {
        }

        public HMACSHA1(byte[] key, bool useManagedSha1)
        {
            m_hashName = "SHA1";
          
            m_hash1 = new SHA1Managed();
            m_hash2 = new SHA1Managed();
          
            HashSizeValue = 160;
            InitializeKey(key);
        }
    }

}
