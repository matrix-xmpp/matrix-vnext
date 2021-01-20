using Matrix.Xml;

namespace Matrix
{
    public class AuthenticationException : XmppException
    {
        public AuthenticationException(XmppXElement stanza) : base(stanza) { }
    }
}
