using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Sasl
{
    public class MechanismsTest
    {
        [Fact]
        public void ShouldBeOfTypeMechanisms()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms1.xml")).ShouldBeOfType<Mechanisms>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms2.xml")).ShouldBeOfType<Mechanisms>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms3.xml")).ShouldBeOfType<Mechanisms>();
        }

        [Fact]
        public void TestMechanisms1()
        {
            var mechs = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms1.xml")).Cast<Mechanisms>();
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.DigestMd5), true);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Plain), true);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Gssapi), true);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Anonymous), false);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.XGoogleToken), false);
        }

        [Fact]
        public void TestMechanisms2()
        {
            var mechanisms = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms2.xml")).ShouldBeOfType<Mechanisms>();
            Assert.Equal(mechanisms.PrincipalHostname, "auth42.us.example.com");
        }

        [Fact]
        public void TestMechanisms3()
        {
            var mechanisms = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms3.xml")).ShouldBeOfType<Mechanisms>();
            Assert.Equal(mechanisms.PrincipalHostname, "auth43.us.example.com");
        }
    }
}
