using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "pubsub", Namespace = Namespaces.Pubsub)]
    public class PubSub : XmppXElement
    {
        #region << Constructors >>
        public PubSub()
            : base(Namespaces.Pubsub, "pubsub")
		{
        }
        #endregion

        public Create Create
        {
            get { return Element<Create>(); }
            set { Replace(value); }
        }

        public Configure Configure
        {
            get { return Element<Configure>(); }
            set { Replace(value); }
        }

        public Subscribe Subscribe
        {
            get { return Element<Subscribe>(); }
            set { Replace(value); }
        }

        public Unsubscribe Unsubscribe
        {
            get { return Element<Unsubscribe>(); }
            set { Replace(value); }
        }
        
        public Publish Publish
        {
            get { return Element<Publish>(); }
            set { Replace(value); }
        }

        public Retract Retract
        {
            get { return Element<Retract>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the affiliations.
        /// </summary>
        /// <value>
        /// The affiliations.
        /// </value>
        public Affiliations Affiliations
        {
            get { return Element<Affiliations>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the subscriptions.
        /// </summary>
        /// <value>
        /// The subscriptions.
        /// </value>
        public Subscriptions Subscriptions
        {
            get { return Element<Subscriptions>(); }
            set { Replace(value); }
        }
        
        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public Options Options
        {
            get { return Element<Options>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public Items Items
        {
            get { return Element<Items>(); }
            set { Replace(value); }
        }
    }
}