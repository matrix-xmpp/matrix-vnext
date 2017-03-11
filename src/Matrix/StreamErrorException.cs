using Matrix.Xml;

namespace Matrix
{
    public class StreamErrorException : XmppException
    {
        public StreamErrorException(XmppXElement stanza) : base(stanza) { }
    }
}
