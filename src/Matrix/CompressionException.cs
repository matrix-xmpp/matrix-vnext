using Matrix.Xml;

namespace Matrix
{
    public class CompressionException : XmppException
    {
        public CompressionException(XmppXElement stanza) : base(stanza) { }
    }
}
