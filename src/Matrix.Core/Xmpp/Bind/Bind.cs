using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Bind
{
    [XmppTag(Name = Tag.Bind, Namespace = Namespaces.Bind)]
    public class Bind : XmppXElement
    {
        //<bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'>
        //    <required/>
        //</bind>

    	// SENT: <iq id="jcl_1" type="set">
		//			<bind xmlns="urn:ietf:params:xml:ns:xmpp-bind"><resource>Exodus</resource></bind>
		//		 </iq>
		// RECV: <iq id='jcl_1' type='result'>
		//			<bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'><jid>gnauck@jabber.ru/Exodus</jid></bind>
        //		 </iq>

        #region << Constructors >>
        public Bind()
            : base(Namespaces.Bind, Tag.Bind)
		{			
		}

		public Bind(string resource) : this()
		{		
			Resource	= resource;
		}

		public Bind(Jid jid) : this()
		{			
			Jid		= jid;
        }
        #endregion

        /// <summary>
        /// Is Bind required (for stream feature only)?
        /// </summary>
        public bool Required
        {
            get { return HasTag("required"); }
            set
            {
                if (value == false)
                    RemoveTag("required");
                else
                    SetTag("required");
            }
        }

        /// <summary>
		/// The resource to bind
		/// </summary>
		public string Resource
		{
			get { return GetTag("resource"); }
			set { SetTag("resource", value); }
		}

		/// <summary>
		/// The jid the server created
		/// </summary>
		public Jid Jid
		{
			get { return new Jid(GetTag("jid")); }
			set { SetTag("jid", value.ToString()); }
		}
	}
}
