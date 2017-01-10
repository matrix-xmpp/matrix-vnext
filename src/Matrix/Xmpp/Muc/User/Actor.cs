using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Muc.User
{
    #region Schema
    /*
      <xs:element name='actor'>
        <xs:complexType>
          <xs:simpleContent>
            <xs:extension base='empty'>
              <xs:attribute name='jid' type='xs:string' use='required'/>
            </xs:extension>
          </xs:simpleContent>
        </xs:complexType>
      </xs:element>
    */
    #endregion

    /// <summary>
    /// Actor
    /// </summary>
    [XmppTag(Name = "actor", Namespace = Namespaces.MucUser)]
    public class Actor : XmppXElementWithJidAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        public Actor()
            : base(Namespaces.MucUser, "actor")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        public Actor(Jid jid)
            : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        public string Nickname
        {
            get { return GetAttribute("nick"); }
            set { SetAttribute("nick", value); }
        }
    }
}