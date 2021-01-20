using Matrix.Xml;

namespace Matrix
{  
    public class StreamManagementException : XmppException
    {       
        public StreamManagementException(XmppXElement stanza) : base(stanza) { }

        public StreamManagementException(XmppXElement stanza, string message) : base(stanza, message) {}
    } 
}
