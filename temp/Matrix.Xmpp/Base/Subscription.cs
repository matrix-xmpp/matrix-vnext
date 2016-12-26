using Matrix.Xml;
using Matrix.Xmpp.PubSub;

namespace Matrix.Xmpp.Base
{
    public abstract class Subscription : XmppXElement
    {
        protected Subscription(string ns) : base(ns, "subscription")
        {
        }

        /// <summary>
        /// Gets or sets the jid.
        /// </summary>
        /// <value>The jid.</value>
        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }

        /// <summary>
        /// Gets or sets the subscription id.
        /// </summary>
        /// <value>The subscription id.</value>
        public string Id
        {
            get { return GetAttribute("subid"); }
            set { SetAttribute("subid", value); }
        }

        /// <summary>
        /// Gets or sets the state of the subscription state.
        /// </summary>
        /// <value>The state of the subscription.</value>
        public SubscriptionState SubscriptionState
        {
            get { return GetAttributeEnum<SubscriptionState>("subscription"); }
            set { SetAttribute("subscription", value.ToString().ToLower()); }
        }
    }
}