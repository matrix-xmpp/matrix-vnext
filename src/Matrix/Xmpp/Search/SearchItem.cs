using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Search
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.IqSearch)]
    public class SearchItem : XmppXElementWithJidAttribute
    {
        #region Schema
        /*
          <xs:element name='item'>
            <xs:complexType>
              <xs:all>
                <xs:element name='first' type='xs:string'/>
                <xs:element name='last' type='xs:string'/>
                <xs:element name='nick' type='xs:string'/>
                <xs:element name='email' type='xs:string'/>
              </xs:all>
              <xs:attribute name='jid' type='xs:string' use='required'/>
            </xs:complexType>
          </xs:element>
        */
        #endregion

        public SearchItem() : base(Namespaces.IqSearch, "item")
        {}

        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The firstname.
        /// </value>
        public string First
        {
            get { return GetTag("first"); }
            set { SetTag("first", value); }
        }
        
        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value>
        public string Last
        {
            get { return GetTag("last"); }
            set { SetTag("last", value); }
        }
        
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        public string Nick
        {
            get { return GetTag("nick"); }
            set { SetTag("nick", value); }
        }
        
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email
        {
            get { return GetTag("email"); }
            set { SetTag("email", value); }
        }
    }
}