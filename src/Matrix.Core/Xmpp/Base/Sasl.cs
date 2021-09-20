namespace Matrix.Xmpp.Base
{
    public abstract class Sasl : XmppXElementWithBased64Value
    {
        protected Sasl(string tag) : base(Namespaces.Sasl, tag)
        {
        }
    }
}
