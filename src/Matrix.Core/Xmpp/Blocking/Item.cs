using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Blocking
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.Blocking)]
    public class Item : XmppXElementWithJidAttribute
    {
        public Item() : base(Namespaces.Blocking, "item")
        {}
    }
}
