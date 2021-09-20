namespace Matrix.Crypt.Wasm
{
    using System;
    using System.Security.Cryptography;

    internal abstract class HMAC : KeyedHashAlgorithm
    {
        // Fields
        private int blockSizeValue = 0x40;
        internal HashAlgorithm m_hash1;
        internal HashAlgorithm m_hash2;
        private bool m_hashing;
        internal string m_hashName;
        private byte[] m_inner;
        private byte[] m_outer;

        // Methods
        protected HMAC()
        {
        }

        //public static HMAC Create()
        //{
        //    return Create("Matrix.Crypt.Hash.HMAC");
        //}

        //public static HMAC Create(string algorithmName)
        //{
        //    return (HMAC)CryptoConfig.CreateFromName(algorithmName);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_hash1 != null)
                {
                    m_hash1.Clear();
                }
                if (m_hash2 != null)
                {
                    m_hash2.Clear();
                }
                if (m_inner != null)
                {
                    Array.Clear(m_inner, 0, m_inner.Length);
                }
                if (m_outer != null)
                {
                    Array.Clear(m_outer, 0, m_outer.Length);
                }
            }
            base.Dispose(disposing);
        }

        protected override void HashCore(byte[] rgb, int ib, int cb)
        {
            if (!m_hashing)
            {
                m_hash1.TransformBlock(m_inner, 0, m_inner.Length, m_inner, 0);
                m_hashing = true;
            }
            m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
        }

        protected override byte[] HashFinal()
        {
            if (!m_hashing)
            {
                m_hash1.TransformBlock(m_inner, 0, m_inner.Length, m_inner, 0);
                m_hashing = true;
            }
            m_hash1.TransformFinalBlock(new byte[0], 0, 0);
            byte[] hashValue = m_hash1.Hash;
            m_hash2.TransformBlock(m_outer, 0, m_outer.Length, m_outer, 0);
            m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
            m_hashing = false;
            m_hash2.TransformFinalBlock(new byte[0], 0, 0);
            return m_hash2.Hash;
        }

        public override void Initialize()
        {
            m_hash1.Initialize();
            m_hash2.Initialize();
            m_hashing = false;
        }

        internal void InitializeKey(byte[] key)
        {
            m_inner = null;
            m_outer = null;
            if (key.Length > BlockSizeValue)
            {
                KeyValue = m_hash1.ComputeHash(key);
            }
            else
            {
                KeyValue = (byte[])key.Clone();
            }
            UpdateIOPadBuffers();
        }

        private void UpdateIOPadBuffers()
        {
            int num;
            if (m_inner == null)
            {
                m_inner = new byte[BlockSizeValue];
            }
            if (m_outer == null)
            {
                m_outer = new byte[BlockSizeValue];
            }
            for (num = 0; num < BlockSizeValue; num++)
            {
                m_inner[num] = 0x36;
                m_outer[num] = 0x5c;
            }
            for (num = 0; num < KeyValue.Length; num++)
            {
                m_inner[num] = (byte)(m_inner[num] ^ KeyValue[num]);
                m_outer[num] = (byte)(m_outer[num] ^ KeyValue[num]);
            }
        }

        // Properties
        protected int BlockSizeValue
        {
            get
            {
                return blockSizeValue;
            }
            set
            {
                blockSizeValue = value;
            }
        }

        public string HashName
        {
            get
            {
                return m_hashName;
            }
            set
            {
                if (m_hashing)
                {
                    throw new CryptographicException("Cryptography_HashNameSet");
                }
                m_hashName = value;
                m_hash1 = HashAlgorithm.Create(m_hashName);
                m_hash2 = HashAlgorithm.Create(m_hashName);
            }
        }

        public override byte[] Key
        {
            get
            {
                return (byte[])KeyValue.Clone();
            }
            set
            {
                if (m_hashing)
                {
                    throw new CryptographicException("Cryptography_HashKeySet");
                }
                InitializeKey(value);
            }
        }
    }


}
