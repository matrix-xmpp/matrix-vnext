using Matrix.Xml;

namespace Matrix.Xmpp.MessageCarbons
{
    public abstract class CarbonBase : XmppXElement
    {
        protected CarbonBase(string tag) : base(Namespaces.MessageCarbons, tag)
        {
        }
    }
}
