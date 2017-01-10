using Matrix.Attributes;

namespace Matrix.Xmpp.Compression
{
    public enum Methods
    {
        [Name("")]
        Unknown = -1,
        
        [Name("zlib")]
        Zlib,

        [Name("lzw")]
        Lzw
    }
}