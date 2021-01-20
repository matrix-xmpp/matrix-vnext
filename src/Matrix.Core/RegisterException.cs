using Matrix.Xml;

namespace Matrix
{
    /// <summary>
    /// This exception is thrown on failures during new account creation (XEP-0077)
    /// </summary>
    public class RegisterException : XmppException
    {
        public RegisterException(XmppXElement stanza) : base(stanza) { }
    }
}
