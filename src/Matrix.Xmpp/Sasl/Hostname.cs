using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// Hostname for XEP-0233: Domain-Based Service Names in XMPP SASL Negotiation.
    /// </summary>
    [XmppTag(Name = "hostname", Namespace = Namespaces.SaslHostname0)]
    [XmppTag(Name = "hostname", Namespace = Namespaces.SaslHostname1)]
    public class Hostname : XmppXElement
    {
        #region Xml sample
        /*
            <mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
              <mechanism>GSSAPI</mechanism>
              <mechanism>DIGEST-MD5</mechanism>
              <hostname xmlns='urn:xmpp:domain-based-name:1'>auth42.us.example.com</hostname>
            </mechanisms> 
        */
        #endregion

        public Hostname() : base(Namespaces.SaslHostname1, "hostname")
        {
        }
    }
}