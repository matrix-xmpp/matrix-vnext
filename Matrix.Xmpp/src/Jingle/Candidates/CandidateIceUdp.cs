using System.Net;
using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Jingle.Candidates
{
    [XmppTag(Name = "candidate", Namespace = Namespaces.JingleTransportIceUdp)]
    public class CandidateIceUdp : CandidateRawUdp
    {
        #region << XML schema >>
        /*
         <xs:complexType name='candidateElementType'>
            <xs:simpleContent>
              <xs:extension base='empty'>
                <xs:attribute name='component' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='foundation' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='generation' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='id' type='xs:NCName' use='required'/>
                <xs:attribute name='ip' type='xs:string' use='required'/>
                <xs:attribute name='network' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='port' type='xs:unsignedShort' use='required'/>
                <xs:attribute name='priority' type='xs:positiveInteger' use='required'/>
                <xs:attribute name='protocol' type='xs:NCName' use='required'/>
                <xs:attribute name='rel-addr' type='xs:string' use='optional'/>
                <xs:attribute name='rel-port' type='xs:unsignedShort' use='optional'/>
                <xs:attribute name='type' use='required'>
                  <xs:simpleType>
                    <xs:restriction base='xs:NCName'>
                      <xs:enumeration value='host'/>
                      <xs:enumeration value='prflx'/>
                      <xs:enumeration value='relay'/>
                      <xs:enumeration value='srflx'/>
                    </xs:restriction>
                  </xs:simpleType>
                </xs:attribute>
              </xs:extension>
            </xs:simpleContent>
         </xs:complexType>
         */
        #endregion

        public CandidateIceUdp() : base(Namespaces.JingleTransportIceUdp)
        {}

        public int Foundation
        {
            get { return GetAttributeInt("foundation"); }
            set { SetAttribute("foundation", value); }
        }

        public int Network
        {
            get { return GetAttributeInt("network"); }
            set { SetAttribute("network", value); }
        }
        
        public int Priority
        {
            get { return GetAttributeInt("priority"); }
            set { SetAttribute("priority", value); }
        }

        public int RelatedPort
        {
            get { return GetAttributeInt("rel-port"); }
            set { SetAttribute("rel-port", value); }
        }

        public IPAddress RelatedIPAddress
        {
            get { return GetAttributeIPAddress("rel-addr"); }
            set { SetAttribute("rel-addr", value.ToString()); }
        }

        public Protocol Protocol
        {
            get { return GetAttributeEnum<Protocol>("protocol"); }
            set { SetAttribute("protocol", value.ToString().ToLower()); }
        }
    }
}