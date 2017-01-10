using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Sasl
{
    
    public class MechanismsTest
    {

        //const  string XML1 = "<stream:features><mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'><mechanism kerb:principal='xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM' xmlns:kerb='http://jabber.com/protocol/kerberosinfo'>GSSAPI</mechanism></mechanisms></stream:features>";//const  string XML1 = "<stream:features><mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'><mechanism kerb:principal='xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM' xmlns:kerb='http://jabber.com/protocol/kerberosinfo'>GSSAPI</mechanism></mechanisms></stream:features>";
        private const string XML1 =
            @"<mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
        <mechanism>DIGEST-MD5</mechanism>
        <mechanism>PLAIN</mechanism>
        <mechanism kerb:principal='xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM' xmlns:kerb='http://jabber.com/protocol/kerberosinfo'>GSSAPI</mechanism>
    </mechanisms>";

        private const string XML2 =
            @"<mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
  <mechanism>GSSAPI</mechanism>
  <mechanism>DIGEST-MD5</mechanism>
  <hostname xmlns='urn:xmpp:domain-based-name:1'>auth42.us.example.com</hostname>
</mechanisms>";

        private const string XML3 =
            @"<mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
  <mechanism>GSSAPI</mechanism>
  <mechanism>DIGEST-MD5</mechanism>  
<hostname xmlns='urn:xmpp:domain-based-name:0'>auth43.us.example.com</hostname>
</mechanisms>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            xmpp1.ShouldBeOfType<Mechanisms>();
            
            var mechs = xmpp1 as Mechanisms;
            if (mechs != null)
            {
                Assert.Equal(mechs.SupportsMechanism(SaslMechanism.DigestMd5), true);
                Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Plain), true);
                Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Gssapi), true);
                Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Anonymous), false);
                Assert.Equal(mechs.SupportsMechanism(SaslMechanism.XGoogleToken), false);
            }

            // TODO
            //string princ = mechs.GetMechanism(SaslMechanism.Gssapi).KerberosPrincipal;
            //Assert.Equal(princ, "xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM");
        }

        [Fact]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            XmppXElement xmpp2 = XmppXElement.LoadXml(XML3);

            xmpp1.ShouldBeOfType<Mechanisms>();
            xmpp2.ShouldBeOfType<Mechanisms>();
            
            var mechs1 = xmpp1 as Mechanisms;
            if (mechs1 != null)
                Assert.Equal(mechs1.PrincipalHostname, "auth42.us.example.com");

            var mechs2 = xmpp2 as Mechanisms;
            if (mechs2 != null)
                Assert.Equal(mechs2.PrincipalHostname, "auth43.us.example.com");
        }
    }
}
