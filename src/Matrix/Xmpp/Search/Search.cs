using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Search
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqSearch)]
    public class Search : XmppXElementWithResultSetAndXDataAndItemCollection<SearchItem>
    {
        #region Schema
        /*
        <xs:element name='query'>
            <xs:complexType>
              <xs:choice>
                <xs:all xmlns:xdata='jabber:x:data'>
                  <xs:element name='instructions' type='xs:string'/>
                  <xs:element name='first' type='xs:string'/>
                  <xs:element name='last' type='xs:string'/>
                  <xs:element name='nick' type='xs:string'/>
                  <xs:element name='email' type='xs:string'/>
                  <xs:element ref='xdata:x' minOccurs='0'/>
                </xs:all>
                <xs:element ref='item' minOccurs='0' maxOccurs='unbounded'/>
              </xs:choice>
            </xs:complexType>
          </xs:element>
        */
        #endregion

        public Search()
            : base(Namespaces.IqSearch, Tag.Query)
        {
        }
        
        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>
        /// The instructions.
        /// </value>
        public string Instructions
        {
            get { return GetTag("instructions"); }
            set { SetTag("instructions", value); }
        }

        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The first.
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
        /// The last.
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
        /// The nick.
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