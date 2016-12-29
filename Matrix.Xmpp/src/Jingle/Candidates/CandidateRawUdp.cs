using System;
using System.Net;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Jingle.Candidates
{
    [XmppTag(Name = "candidate", Namespace = Namespaces.JingleTransportRawUdp)]
    public class CandidateRawUdp : XmppXElement
    {
        #region < XML schema >>
        /*
         <xs:complexType name='candidateElementType'>
            <xs:simpleContent>
              <xs:extension base='empty'>
                <xs:attribute name='component' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='generation' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='id' type='xs:NCName' use='required'/>
                <xs:attribute name='ip' type='xs:string' use='required'/>
                <xs:attribute name='port' type='xs:unsignedShort' use='required'/>
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
        internal CandidateRawUdp(string ns) : base(ns, "candidate")
        {
            
        }
        public CandidateRawUdp() : this(Namespaces.JingleTransportRawUdp)
        {
        }

        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }

        public int Component
        {
            get { return GetAttributeInt("component"); }
            set { SetAttribute("component", value); }
        }

        public int Generation
        {
            get { return GetAttributeInt("generation"); }
            set { SetAttribute("generation", value); }
        }

        public int Port
        {
            get { return GetAttributeInt("port"); }
            set { SetAttribute("port", value); }
        }

        public CandidateType Type
        {
            get { return GetAttributeEnum<CandidateType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

#if WINRT || CF || WP7
        public string IPAddress
        {
            get { return GetAttribute("ip"); }
            set { SetAttribute("ip", value); }
        }
#else
        public IPAddress IPAddress
        {
            get { return GetAttributeIPAddress("ip"); }
            set { SetAttribute("ip", value.ToString()); }
        }
#endif

        /// <summary>
        /// generates a new unique Sid
        /// </summary>
        public void GenerateId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}