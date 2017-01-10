using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "changed", Namespace = Namespaces.Archiving)]
    public class Changed : ArchiveEvent
    {
        #region
        /*
          <xs:element name='changed'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='start' type='xs:dateTime' use='required'/>
                  <xs:attribute name='with' type='xs:string' use='required'/>
                  <xs:attribute name='version' type='xs:nonNegativeInteger' use='required'/>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion
        public Changed() : base("changed")
        {
        }
    }
}