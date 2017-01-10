using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Nickname
{
    /// <summary>
    /// XEP-0172: User Nickname
    /// </summary>
    [XmppTag(Name = "nick", Namespace = Namespaces.Nick)]
    public class Nick : XmppXElement
    {
        #region Xml smaple
        /*
	        <nick xmlns='http://jabber.org/protocol/nick'>Ishmael</nick>
        */
        #endregion

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Nick"/> class.
        /// </summary>
		public Nick() : base(Namespaces.Nick, "nick")
		{		
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Nick"/> class.
        /// </summary>
        /// <param name="nickname">The nickname.</param>
		public Nick(string nickname) : this()
		{            
			Value = nickname;
		}
		#endregion
		
		static public implicit operator Nick(string value)
	    {            
	        return new Nick(value);
	    }
	
	    static public implicit operator string(Nick nick)
	    {
	        return nick.Value;
	    }
	}
}