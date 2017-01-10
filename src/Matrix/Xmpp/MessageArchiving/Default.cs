using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "default", Namespace = Namespaces.Archiving)]
    public class Default : XmppXElement
    {
        #region Schema
        /*
          <xs:element name='default'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='expire' type='xs:nonNegativeInteger' use='optional'/>
                  <xs:attribute name='otr' use='required'>
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
                  <xs:attribute name='save' use='required'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='body'/>
                        <xs:enumeration value='false'/>
                        <xs:enumeration value='message'/>
                        <xs:enumeration value='stream'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                  <xs:attribute name='unset' use='optional' type='xs:boolean'/>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion
       
        public Default() : base(Namespaces.Archiving, "default")
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
        /// Specifies the user's default setting for Save Mode
        /// </summary>
        public SaveType Save
        {
            get { return GetAttributeEnum<SaveType>("save"); }
            set { SetAttribute("save", value.ToString().ToLower()); }
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

        /// <summary>
        /// If the user has never set the default Modes, the 'Save' and 'Otr' values SHOULD specify the server's default settings,
        /// and the 'unset' value SHOULD be set to 'true'.
        /// Note: The 'unset' value defaults to 'false'.
        /// </summary>
        public bool Unset
        {
            get { return GetAttributeBool("unset"); }
            set { SetAttribute("unset", value); }
        }
    }
}