using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiveManagement
{
    [XmppTag(Name = "jid", Namespace = Namespaces.MessageArchiveManagement)]
    public class Jid : XmppXElement
    {
        public Jid() : base(Namespaces.MessageArchiveManagement, "jid")
        {
        }

        public Jid(Matrix.Jid val) : this()
        {
            this.Value = val.Bare;
        }
    }
}