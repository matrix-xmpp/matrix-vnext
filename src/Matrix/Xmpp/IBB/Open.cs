using Matrix.Attributes;

namespace Matrix.Xmpp.IBB
{
    [XmppTag(Name = "open", Namespace = Namespaces.Ibb)]
    public class Open : Close
    {
        public Open() : base("open")
        {
        }

        internal Open(string ns, string tagname) : base(ns, tagname)
        {
        }

        /// <summary>
        /// Block size
        /// </summary>
        public long BlockSize
        {
            get { return GetAttributeLong("block-size"); }
            set { SetAttribute("block-size", value); }
        }

        /// <summary>
        /// Defines whether the data will be sent using iq stanzas or message stanzas.
        /// </summary>
        public StanzaType Stanza
        {
            get { return GetAttributeEnum<StanzaType>("stanza"); }
            set { SetAttribute("stanza", value.ToString().ToLower()); }
        }
    }
}