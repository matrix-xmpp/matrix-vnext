using Matrix.Core.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "retrieve", Namespace = Namespaces.Archiving)]
    public class Retrieve : Link
    {
        #region Schema
        /*
          <xs:element name='retrieve'>
            <xs:complexType>
              <xs:sequence>
                <xs:any processContents='lax' namespace='##other' minOccurs='0' maxOccurs='unbounded'/>
              </xs:sequence>
              <xs:attribute name='start' type='xs:dateTime' use='required'/>
              <xs:attribute name='with' type='xs:string' use='required'/>
            </xs:complexType>
          </xs:element>
         */
        #endregion

        public Retrieve() : base("retrieve")
        {
        }
    }
}