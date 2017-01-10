using Matrix.Core.Attributes;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "unsubscribe", Namespace = Namespaces.Pubsub)]
    public class Unsubscribe : Subscribe
    {
        #region << Constructors >>
        public Unsubscribe(): base("unsubscribe")
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the subscription id.
        /// </summary>
        /// <value>The subscription id.</value>
        public string SubId
        {
            get { return GetAttribute("subid"); }
            set { SetAttribute("subid", value); }
        }    
    }
}