using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Jingle.Apps.Rtp
{
    [XmppTag(Name = "payload-type", Namespace = Namespaces.JingleAppsRtp)]
    public class PayloadType : XmppXElement
    {
        #region Schema
        /*
            <xs:complexType name='payloadElementType'>
                <xs:sequence>
                  <xs:element name='parameter'
                              type='parameterElementType'
                              minOccurs='0' 
                              maxOccurs='unbounded'/>
                </xs:sequence>
                <xs:attribute name='channels'
                              type='xs:unsignedByte'
                              use='optional'
                              default='1'/>
                <xs:attribute name='clockrate' type='xs:unsignedInt' use='optional'/>
                <xs:attribute name='id' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='maxptime' type='xs:unsignedInt' use='optional'/>
                <xs:attribute name='name' type='xs:string' use='optional'/>
                <xs:attribute name='ptime' type='xs:unsignedInt' use='optional'/>
            </xs:complexType>
          */
        #endregion

        public PayloadType()
            : base(Namespaces.JingleAppsRtp, "payload-type")
        { }

        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }
 
        public int Id
        {
            get { return GetAttributeInt("id"); }
            set { SetAttribute("id", value); }
        }

        public int Clockrate
        {
            get { return GetAttributeInt("clockrate"); }
            set { SetAttribute("clockrate", value); }
        }

        public int PTime
        {
            get { return GetAttributeInt("ptime"); }
            set { SetAttribute("ptime", value); }
        }

        public int MaxPTime
        {
            get { return GetAttributeInt("maxptime"); }
            set { SetAttribute("maxptime", value); }
        }
    }
}