using Matrix.Xml;
using Matrix.Xmpp.AdvancedMessageProcessing;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.AdvancedMessageProcessing
{
    public class AmpTest
    {
        [Fact]
        public void TestXmlShouldBeOfTypeAmp()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp1.xml")).ShouldBeOfType<Amp>();
        }

        [Fact]
        public void PerHopShouldBeTrue()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp1.xml"))
                .Cast<Amp>()
                .PerHop.ShouldBeTrue();
        }

        [Fact]
        public void BuildAmpElement()
        {
            string expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.amp3.xml");
            new Amp().ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildAmpElementWithPerHopAttribute()
        {
            string expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.amp1.xml");
            new Amp { PerHop = true }.ShouldBe(expectedXml);
        }

        [Fact]
        public void TestFrom()
        {
            var amp = XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp2.xml")).Cast<Amp>();
            amp.From.Equals("bernardo@hamlet.lit/elsinore").ShouldBeTrue(); 
        }

        [Fact]
        public void TestTo()
        {
            var amp = XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp2.xml")).Cast<Amp>();
            amp.To.Equals("francisco@hamlet.lit/pda").ShouldBeTrue();
        }

        [Fact]
        public void BuildAmpWithToFromStatus()
        {
            string expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.amp4.xml");
            new Amp
            {
                Status = Matrix.Xmpp.AdvancedMessageProcessing.Action.Alert,
                From = "bernardo@hamlet.lit/elsinore",
                To = "francisco@hamlet.lit"
            }
            .ShouldBe(expectedXml);
        }
    }
}
