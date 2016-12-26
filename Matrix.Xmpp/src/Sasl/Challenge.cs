using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Sasl
{
    [XmppTag(Name = "challenge", Namespace = Namespaces.Sasl)]
    public class Challenge: Base.Sasl
    {
        public Challenge() : base("challenge")
        {
        }

        public Challenge(string content) : this()            
        {
            Value = content;
        }
    }
}