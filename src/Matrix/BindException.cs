using Matrix.Xml;

namespace Matrix
{
    public class BindException : XmppException
    {
        public BindException(XmppXElement stanza) : base(stanza) { }
    }
}
