using System.Collections.Generic;
using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class Subscriptions : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriptions"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        internal protected Subscriptions(string ns) : base(ns, "subscriptions")
        {
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttributeJid("node"); }
            set { SetAttribute("node", value); }
        }


        /// <summary>
        /// Adds a subscription.
        /// </summary>
        /// <param name="subcription">The sub.</param>
        public void AddSubscription(Subscription subcription)
        {
            Add(subcription);
        }

        /// <summary>
        /// Sets the subscriptions.
        /// </summary>
        /// <param name="subcriptions">The subcriptions.</param>
        public void SetSubscriptions(IEnumerable<Subscription> subcriptions)
        {
            RemoveAllSubscriptions();
            foreach (Subscription sub in subcriptions)
                AddSubscription(sub);
        }

        /// <summary>
        /// Removes all subscriptions.
        /// </summary>
        public void RemoveAllSubscriptions()
        {
            RemoveAll<Subscription>();
        }
    }
}
