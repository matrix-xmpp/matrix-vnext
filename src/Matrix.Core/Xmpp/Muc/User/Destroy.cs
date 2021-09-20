using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Muc.User
{
    /// <summary>
    /// Destroy
    /// </summary>
    [XmppTag(Name = "destroy", Namespace = Namespaces.MucUser)]
    public class Destroy : XmppXElement
    {
        /*
          <xs:element name='destroy'>
            <xs:complexType>
              <xs:sequence>
                <xs:element ref='reason' minOccurs='0'/>
              </xs:sequence>
              <xs:attribute name='jid' type='xs:string' use='optional'/>
            </xs:complexType>
          </xs:element>
         
         <presence
            from='heath@chat.shakespeare.lit/thirdwitch'
            to='hag66@shakespeare.lit/pda'
            type='unavailable'>
          <x xmlns='http://jabber.org/protocol/muc#user'>
            <item affiliation='none' role='none'/>
            <destroy jid='darkcave@chat.shakespeare.lit'>
              <reason>Macbeth doth come.</reason>
            </destroy>
          </x>
        </presence>
        */

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Destroy"/> class.
        /// </summary>
        public Destroy()
            : this(Namespaces.MucUser)
        {
        }

        internal Destroy(string ns) : base(ns, "destroy")
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Destroy"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        public Destroy(Jid jid)
            : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Destroy"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="reason">The reason.</param>
        public Destroy(Jid jid, string reason)
            : this(jid)
        {
            Reason = reason;
        }
        #endregion        


        /// <summary>
        /// Gets or sets the <see cref="Jid"/>.
        /// </summary>
        /// <value>The jid.</value>
        public Jid Jid
        {
            get { return GetAttribute("jid"); }
            set { SetAttribute("jid", value); }
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason
        {
            set { SetTag("reason", value); }
            get { return GetTag("reason"); }
        }
    }
}
