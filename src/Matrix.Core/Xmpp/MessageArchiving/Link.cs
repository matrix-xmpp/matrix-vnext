using System;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.MessageArchiving
{
    /// <summary>
    /// Base class with "Start" and "With" attribute
    /// </summary>
    public abstract class Link : XmppXElementWithResultSet
    {
        protected Link(string tagName) : base(Namespaces.Archiving, tagName)
        {
        }
        
        public Jid With
        {
            get { return GetAttributeJid("with"); }
            set { SetAttribute("with", value); }
        }

        public DateTime Start
        {
            get { return GetAttributeIso8601Date("start"); }
            set { SetAttributeIso8601Date("start", value); }
        }
    }
}
