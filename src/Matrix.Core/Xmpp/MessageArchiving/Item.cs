using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.Archiving)]
    public class Item : XmppXElement
    {
        #region
        /*          
          <xs:element name='item'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='exactmatch' type='xs:boolean' use='optional'/>
                  <xs:attribute name='expire' type='xs:nonNegativeInteger' use='optional'/>
                  <xs:attribute name='jid' use='required' type='xs:string'/>
                  <xs:attribute name='otr' use='optional'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='approve'/>
                        <xs:enumeration value='concede'/>
                        <xs:enumeration value='forbid'/>
                        <xs:enumeration value='oppose'/>
                        <xs:enumeration value='prefer'/>
                        <xs:enumeration value='require'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                  <xs:attribute name='save' use='optional'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='body'/>
                        <xs:enumeration value='false'/>
                        <xs:enumeration value='message'/>
                        <xs:enumeration value='stream'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion

        public Item() : base(Namespaces.Archiving, "item")
        {
        }

        /// <summary>
        /// Specifies the user's default setting for OTR Mode.
        /// </summary>
        public OtrType Otr
        {
            get { return GetAttributeEnum<OtrType>("otr"); }
            set { SetAttribute("otr", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Specifies the user's default setting for Save Mode. The allowable values are:
        /// </summary>
        public new SaveType Save
        {
            get { return GetAttributeEnum<SaveType>("save"); }
            set { SetAttribute("save", value.ToString().ToLower()); }
        }

        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }
        
        /// <summary>
        /// If "Save" is not set to 'false' then is RECOMMENDED to also include an "Expire" value, which indicates 
        /// how many seconds after messages are archived that the server SHOULD delete them.
        /// </summary>
        public int Expire
        {
            get { return GetAttributeInt("expire"); }
            set { SetAttribute("expire", value); }
        }
        
        public bool ExactMatch
        {
            get { return GetAttributeBool("exactmatch"); }
            set { SetAttribute("exactmatch", value); }
        }
    }
}
