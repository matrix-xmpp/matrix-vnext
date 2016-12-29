using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Bytestreams
{
    public enum Mode
    {
        None = -1,

        [Name("tcp")]
        Tcp,
        
        [Name("udp")]
        Udp
    }
}