using Matrix.Attributes;

namespace Matrix.Xmpp.RosterItemExchange
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.XRosterX)]
    public class RosterExchangeItem : Base.RosterItem
    {
        public RosterExchangeItem() : base(Namespaces.XRosterX)
        {
        }

        public Action Action
        {
            get
            {
                return !HasAttribute("action") ? Action.Add : GetAttributeEnum<Action>("action");
            }
            set { SetAttribute("action", value.ToString()); }
        }
    }
}
