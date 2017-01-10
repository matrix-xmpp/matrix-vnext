using Matrix.Xml;
using Matrix.Xmpp.ResultSetManagement;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithResultSet : XmppXElement
    {
        protected XmppXElementWithResultSet(string ns, string tagname) : base(ns, tagname)
        {
        }
        
        /// <summary>
        /// Gets or sets the result set.
        /// </summary>
        /// <value>
        /// The result set.
        /// </value>
        public Set ResultSet
        {
            get { return Element<Set>(); }
            set { Replace(value); }
        }
    }
}