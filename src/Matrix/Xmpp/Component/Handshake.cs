using Matrix.Attributes;
using Matrix.Crypt;
using Matrix.Xml;

namespace Matrix.Xmpp.Component
{
    [XmppTag(Name = "handshake", Namespace = Namespaces.Accept)]
    public class Handshake  : XmppXElement
    {
        public Handshake() : base(Namespaces.Accept, "handshake")
        {
        }

        public Handshake(string streamid, string password)
            : this()
        {
            Value = Hash.Sha1HashHex(streamid + password);
        }
    }
}