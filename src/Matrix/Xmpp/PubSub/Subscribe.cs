using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "subscribe", Namespace = Namespaces.Pubsub)]
    public class Subscribe: XmppXElement
    {
        #region Xml sample
        /*
            <iq type='set'
                from='francisco@denmark.lit/barracks'
                to='pubsub.shakespeare.lit'
                id='sub1'>
              <pubsub xmlns='http://jabber.org/protocol/pubsub'>
                <subscribe
                    node='princely_musings'
                    jid='francisco@denmark.lit'/>
              </pubsub>
            </iq>
        */
        #endregion

        #region << Constructors >>
        public Subscribe()
            : this("subscribe")
		{
        }

        internal Subscribe(string tagName) : base(Namespaces.Pubsub, tagName)
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
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
    }
}