using Matrix.Attributes;

namespace Matrix.Xmpp.AdvancedMessageProcessing
{
    [XmppTag(Name = "amp", Namespace = Namespaces.AMP)]
    public class Amp : Base.XmppXElementWithRules
    {
        public Amp() : base(Namespaces.AMP, "amp")
        {}

        /// <summary>
        /// Gets or sets the from Jid.
        /// </summary>
        /// <value>From.</value>
        public Jid From
        {
            get { return GetAttributeJid("from"); }
            set { SetAttribute("from", value); }
        }

        /// <summary>
        /// Gets or sets the to Jid.
        /// </summary>
        /// <value>To.</value>
        public Jid To
        {
            get { return GetAttributeJid("to"); }
            set { SetAttribute("to", value); }
        }

        /// <summary>
        /// The 'per-hop' attribute flags the contained ruleset for processing at each server in the route 
        /// between the original sender and original intended recipient. 
        /// This attribute MAY be present, and MUST be either "true" or "false". 
        /// If not present, the default is "false".
        /// </summary>
        public bool PerHop
        {
            get { return GetAttributeBool("per-hop"); }
            set { SetAttribute("per-hop", value); }
        }

        /// <summary>
        /// The 'status' attribute specifies the reason for the amp element.
        /// When specifying semantics to be applied (client to server), this attribute MUST NOT be present. 
        /// When replying to a sending entity regarding a met condition, this attribute MUST be present and 
        /// SHOULD be the value of the 'action' attribute for the triggered rule. 
        /// (Note: Individual action definitions MAY provide their own requirements.)
        /// </summary>
        public Action Status
        {
            get
            {
                return GetAttributeEnum<Action>("status");
            }
            set
            {
                if (value == Action.None)
                    RemoveAttribute("status");
                else
                    SetAttribute("status", value.ToString().ToLower());
            }
        }
    }
}