using Matrix.Attributes;

namespace Matrix.Xmpp.Bytestreams
{
    /*
      <xs:element name='streamhost'>
        <xs:complexType>
          <xs:simpleContent>
            <xs:extension base='empty'>
              <xs:attribute name='jid' type='xs:string' use='required'/>
              <xs:attribute name='host' type='xs:string' use='required'/>
              <xs:attribute name='zeroconf' type='xs:string' use='optional'/>
              <xs:attribute name='port' type='xs:string' use='optional'/>
            </xs:extension>
          </xs:simpleContent>
        </xs:complexType>
      </xs:element>
    */
    [XmppTag(Name = "streamhost", Namespace = Namespaces.Bytestreams)]
    public class Streamhost : StreamhostUsed
    {
        public Streamhost() : base("streamhost")
        {
        }

        /// <summary>
        /// the hostname or IP address of the StreamHost for SOCKS5 communications over TCP
        /// </summary>
        public string Host
        {
            get { return GetAttribute("host");}
            set { SetAttribute("host", value);}
        }

        /// <summary>
        /// specifies the zero-configuration service available for bytestreaming.
        /// This value SHOULD be present. The value SHOULD be '_jabber.bytestreams'.
        /// </summary>
        public string Zeroconf
        {
            get { return GetAttribute("zeroconf"); }
            set { SetAttribute("zeroconf", value); }
        }

        /// <summary>
        /// a port associated with the hostname or IP address for SOCKS5 communications over TCP
        /// </summary>
        public int Port
        {
            get { return GetAttributeInt("port"); }
            set { SetAttribute("port", value); }
        }
    }
}