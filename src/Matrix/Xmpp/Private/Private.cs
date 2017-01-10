using Matrix.Attributes;
using Matrix.Xml;
using Matrix.Xmpp.Bookmarks;

namespace Matrix.Xmpp.Private
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqPrivate)]
    public class Private : XmppXElement
    {
        public Private() : base(Namespaces.IqPrivate, Tag.Query)
        {
        }

        public Storage Storage
        {
            get { return Element<Storage>(); }
            set { Replace(value); }
        }
    }
}