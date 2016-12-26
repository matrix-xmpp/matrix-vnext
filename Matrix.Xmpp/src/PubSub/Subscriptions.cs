using System.Collections.Generic;
using Matrix.Core.Attributes;

namespace Matrix.Xmpp.PubSub
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "subscriptions", Namespace = Namespaces.Pubsub)]
    public class Subscriptions : Base.Subscriptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriptions"/> class.
        /// </summary>
        public Subscriptions()
            : base(Namespaces.Pubsub)
        {
        }

        /// <summary>
        /// Adds a subscription.
        /// </summary>
        /// <returns></returns>
        public Subscription AddSubscription()
        {
            var sub = new Subscription();
            Add(sub);

            return sub;
        }

        /// <summary>
        /// Gets the subscriptions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Subscription> GetSubscriptions()
        {
            return Elements<Subscription>();
        }
    }
}