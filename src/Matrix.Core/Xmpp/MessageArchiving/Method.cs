using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "method", Namespace = Namespaces.Archiving)]
    public class Method : XmppXElement
    {
        #region Schema
        /*
          <xs:element name='method'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='type' type='xs:string' use='required'/>
                  <xs:attribute name='use' use='required'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='concede'/>
                        <xs:enumeration value='forbid'/>
                        <xs:enumeration value='prefer'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion

        #region Xml samples
        /*
            <method type='auto' use='concede'/>
            <method type='local' use='concede'/>
            <method type='manual' use='concede'/>
         */
        #endregion
        public Method() : base(Namespaces.Archiving, "method")
        {
        }

        public MethodType Type
        {
            get { return GetAttributeEnum<MethodType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }
        
        public UseType Use
        {
            get { return GetAttributeEnum<UseType>("use"); }
            set { SetAttribute("use", value.ToString().ToLower()); }
        }
    }
}
