using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "removed", Namespace = Namespaces.Archiving)]
    public class Removed : ArchiveEvent
    {
        #region Schema
        /*
          <xs:element name='removed'>
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

        public Removed() : base("removed")
        {
        }
    }
}