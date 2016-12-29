using Matrix.Core;

namespace Matrix.Xmpp.MessageArchiving
{
    public abstract class MessageItem : ArchiveItem
    {
        #region Schema
        /*
          <xs:complexType name='messageType'>
            <xs:sequence>
              <xs:element name='body' type='xs:string' minOccurs='0' maxOccurs='unbounded'/>
              <xs:any processContents='lax' namespace='##other' minOccurs='0' maxOccurs='unbounded'/>
            </xs:sequence>
            <xs:attribute name='jid' type='xs:string' use='optional'/>
            <xs:attribute name='name' type='xs:string' use='optional'/>
            <xs:attribute name='secs' type='xs:nonNegativeInteger' use='optional'/>
            <xs:attribute name='utc' type='xs:dateTime' use='optional'/>
          </xs:complexType>
        */
        #endregion

        protected MessageItem(string tagName) : base(tagName)
        {}

        /// <summary>
        /// Gets or sets the content of a message.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body
        {
            get { return GetTag("body"); }
            set { SetTag("body", value); }
        }
        
        /// <summary>
        /// Gets or sets the seconds of the message relative to the previous message in the collection 
        /// (or, for the first message, relative to the start of the collection)
        /// </summary>
        /// <value>
        /// The seconds.
        /// </value>
        public int Seconds
        {
            get { return GetAttributeInt("secs"); }
            set { SetAttribute("secs", value); }
        }

        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }

        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }
    }
}