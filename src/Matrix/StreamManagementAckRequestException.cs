using Matrix.Xml;

namespace Matrix
{
    public class StreamManagementAckRequestException : StreamManagementException
    {
        public StreamManagementAckRequestException(XmppXElement stanza, string message) : base(stanza, message) { }
    }   
}
