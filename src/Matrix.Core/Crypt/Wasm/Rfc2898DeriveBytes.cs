namespace Matrix.Crypt.Wasm
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    internal class Rfc2898DeriveBytes : DeriveBytes
    {
        private const string DEFAULT_HASH_ALGORITHM = "SHA-1";

        private const int MinimumSaltSize = 0;
        private readonly byte[] _password;
        private byte[] _salt;
        private uint _iterations;
        private HMAC _hmac;
        private int _blockSize;

        private byte[] _buffer;
        private uint _block;
        private int _startIndex;
        private int _endIndex;

        public string HashAlgorithm { get; }

        public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
            : this(password, salt, iterations, DEFAULT_HASH_ALGORITHM)
        {
        }

        public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations, string hashAlgorithm)
        {
            if (salt == null)
                throw new ArgumentNullException(nameof(salt));
            if (salt.Length < MinimumSaltSize)
                throw new ArgumentException(nameof(salt));
            if (iterations <= 0)
                throw new ArgumentOutOfRangeException(nameof(iterations));
            if (password == null)
                throw new NullReferenceException();  // This "should" be ArgumentNullException but for compat, we throw NullReferenceException.

            _salt = (byte[])salt.Clone();
            _iterations = (uint)iterations;
            _password = (byte[])password.Clone();
            HashAlgorithm = hashAlgorithm;
            _hmac = OpenHmac();
            // _blockSize is in bytes, HashSize is in bits.
            _blockSize = _hmac.HashSize >> 3;

            Initialize();
        }

        public Rfc2898DeriveBytes(string password, byte[] salt)
             : this(password, salt, 1000)
        {
        }

        public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
            : this(password, salt, iterations, DEFAULT_HASH_ALGORITHM)
        {
        }

        public Rfc2898DeriveBytes(string password, byte[] salt, int iterations, string hashAlgorithm)
            : this(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithm)
        {
        }

        //public Rfc2898DeriveBytes(string password, int saltSize)
        //    : this(password, saltSize, 1000)
        //{
        //}

        //public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
        //    : this(password, saltSize, iterations, DEFAULT_HASH_ALGORITHM)
        //{
        //}

        public Rfc2898DeriveBytes(string password, int saltSize, int iterations, string hashAlgorithm)
        {
            if (saltSize < 0)
                throw new ArgumentOutOfRangeException(nameof(saltSize));
            if (saltSize < MinimumSaltSize)
                throw new ArgumentException(nameof(saltSize));
            if (iterations <= 0)
                throw new ArgumentOutOfRangeException(nameof(iterations));

            _salt = new byte[saltSize];
            RandomNumberGenerator.Create().GetBytes(_salt);
            _iterations = (uint)iterations;
            _password = Encoding.UTF8.GetBytes(password);
            HashAlgorithm = hashAlgorithm;
            _hmac = OpenHmac();
            // _blockSize is in bytes, HashSize is in bits.
            _blockSize = _hmac.HashSize >> 3;

            Initialize();
        }

        public int IterationCount
        {
            get
            {
                return (int)_iterations;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _iterations = (uint)value;
                Initialize();
            }
        }

        public byte[] Salt
        {
            get
            {
                return (byte[])_salt.Clone();
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value.Length < MinimumSaltSize)
                    throw new ArgumentException("Too few bytes for salt");
                _salt = (byte[])value.Clone();
                Initialize();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_hmac != null)
                {
                    _hmac.Dispose();
                    _hmac = null;
                }

                if (_buffer != null)
                    Array.Clear(_buffer, 0, _buffer.Length);
                if (_password != null)
                    Array.Clear(_password, 0, _password.Length);
                if (_salt != null)
                    Array.Clear(_salt, 0, _salt.Length);
            }
            base.Dispose(disposing);
        }

        public override byte[] GetBytes(int cb)
        {
            if (cb <= 0)
                throw new ArgumentOutOfRangeException(nameof(cb));
            byte[] password = new byte[cb];

            int offset = 0;
            int size = _endIndex - _startIndex;
            if (size > 0)
            {
                if (cb >= size)
                {
                    Buffer.BlockCopy(_buffer, _startIndex, password, 0, size);
                    _startIndex = _endIndex = 0;
                    offset += size;
                }
                else
                {
                    Buffer.BlockCopy(_buffer, _startIndex, password, 0, cb);
                    _startIndex += cb;
                    return password;
                }
            }

            while (offset < cb)
            {
                byte[] T_block = Func();
                int remainder = cb - offset;
                if (remainder > _blockSize)
                {
                    Buffer.BlockCopy(T_block, 0, password, offset, _blockSize);
                    offset += _blockSize;
                }
                else
                {
                    Buffer.BlockCopy(T_block, 0, password, offset, remainder);
                    offset += remainder;
                    Buffer.BlockCopy(T_block, remainder, _buffer, _startIndex, _blockSize - remainder);
                    _endIndex += (_blockSize - remainder);
                    return password;
                }
            }
            return password;
        }

        //public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
        //{
        //    // If this were to be implemented here, CAPI would need to be used (not CNG) because of
        //    // unfortunate differences between the two. Using CNG would break compatibility. Since this
        //    // assembly currently doesn't use CAPI it would require non-trivial additions.
        //    // In addition, if implemented here, only Windows would be supported as it is intended as
        //    // a thin wrapper over the corresponding native API.
        //    // Note that this method is implemented in PasswordDeriveBytes (in the Csp assembly) using CAPI.
        //    throw new PlatformNotSupportedException();
        //}

        public override void Reset()
        {
            Initialize();
        }
        
        private HMAC OpenHmac()
        {
            String hashAlgorithm = HashAlgorithm;

            if (string.IsNullOrEmpty(hashAlgorithm))
                throw new CryptographicException("HashAlgorithm name not present");

            if (hashAlgorithm.Equals("SHA-1", StringComparison.OrdinalIgnoreCase))
                return new HMACSHA1(_password);
            ////if (hashAlgorithm.Equals("SHA-256", StringComparison.OrdinalIgnoreCase))
            ////    return new HMACSHA256(_password);
            ////if (hashAlgorithm.Equals("SHA-384", StringComparison.OrdinalIgnoreCase))
            ////    return new HMACSHA384(_password);
            ////if (hashAlgorithm.Equals("SHA-512", StringComparison.OrdinalIgnoreCase))
            ////    return new HMACSHA512(_password);

            throw new CryptographicException("MAC algorithm " + hashAlgorithm + " not available");
        }

        private void Initialize()
        {
            if (_buffer != null)
                Array.Clear(_buffer, 0, _buffer.Length);
            _buffer = new byte[_blockSize];
            _block = 1;
            _startIndex = _endIndex = 0;
        }

        // This function is defined as follows:
        // Func (S, i) = HMAC(S || i) | HMAC2(S || i) | ... | HMAC(iterations) (S || i)
        // where i is the block number.
        private byte[] Func()
        {
            byte[] temp = new byte[_salt.Length + sizeof(uint)];
            Buffer.BlockCopy(_salt, 0, temp, 0, _salt.Length);

            WriteInt(_block, temp, _salt.Length);

            temp = _hmac.ComputeHash(temp);

            byte[] ret = temp;
            for (int i = 2; i <= _iterations; i++)
            {
                temp = _hmac.ComputeHash(temp);

                for (int j = 0; j < _blockSize; j++)
                {
                    ret[j] ^= temp[j];
                }
            }

            // increment the block count.
            _block++;
            return ret;
        }

        private void WriteInt(uint i, byte[] buf, int bufOff)
        {
            buf[bufOff++] = (byte)(i >> 24);
            buf[bufOff++] = (byte)(i >> 16);
            buf[bufOff++] = (byte)(i >> 8);
            buf[bufOff] = (byte)i;
        }
    }

    internal abstract class DeriveBytes
        // On Orcas DeriveBytes is not disposable, so we cannot add the IDisposable implementation to the 
        // CoreCLR mscorlib.  However, this type does need to be disposable since subtypes can and do hold onto 
        // native resources. Therefore, on desktop mscorlibs we add an IDisposable implementation.
        : IDisposable
    {
        // 
        // public methods
        // 

        public abstract byte[] GetBytes(int cb);
        public abstract void Reset();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            return;
        }
    }
}
