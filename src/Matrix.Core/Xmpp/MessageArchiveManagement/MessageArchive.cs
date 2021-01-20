using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiveManagement
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.MessageArchiveManagement)]
    public class MessageArchive : XmppXElement
    {
        protected MessageArchive(string tagName)
            : base(Namespaces.MessageArchiveManagement, tagName)
        {
        }

        public MessageArchive() : base(Namespaces.MessageArchiveManagement, Tag.Query)
        {
        }

        /// <summary>
        /// Get or sets the query id
        /// </summary>
        public string QueryId
        {
            get => GetAttribute("queryid");
            set => SetAttribute("queryid", value);
        }


        /// <summary>
        /// Get or sets the query node
        /// </summary>
        public string Node
        {
            get => GetAttribute("node");
            set => SetAttribute("node", value);
        }
    }
}
