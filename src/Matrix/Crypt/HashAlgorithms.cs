using Matrix.Attributes;

namespace Matrix.Crypt
{
    public enum HashAlgorithms
    {
        [Name("unknown")]
        Unknown,

        [Name("sha-1")]
        Sha1,

        [Name("sha-256")]
        Sha256,

        [Name("sha-384")]
        Sha384,

        [Name("sha-512")]
        Sha512,

        [Name("md5")]
        Md5,
    }
}
