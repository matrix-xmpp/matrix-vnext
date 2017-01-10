using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "create", Namespace = Namespaces.Pubsub)]
    public class Create : XmppXElement
    {
        #region Xml sample
        /*
           <iq type="set"
               from="pgm@jabber.org"
               to="pubsub.jabber.org"
               id="create1">
               <pubsub xmlns="http://jabber.org/protocol/pubsub">
                   <create node="generic/pgm-mp3-player"/>
                   <configure/>
               </pubsub>
           </iq>       
        */
        #endregion

        #region << Constructors >>
        public Create() :base(Namespaces.Pubsub, "create")
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
    }
}