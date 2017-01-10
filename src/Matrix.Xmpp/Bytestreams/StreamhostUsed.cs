using Matrix.Core;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Bytestreams
{
    [XmppTag(Name = "streamhost-used", Namespace = Namespaces.Bytestreams)]
    public class StreamhostUsed : XmppXElement
    {
        public  StreamhostUsed() : this("streamhost-used")
        {
        }

        internal StreamhostUsed(string tagname) : base(Namespaces.Bytestreams, tagname)
        {
        }

        public StreamhostUsed(Jid jid) : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// The Jabber id of the streamhost
        /// </summary>
        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }
    }
}