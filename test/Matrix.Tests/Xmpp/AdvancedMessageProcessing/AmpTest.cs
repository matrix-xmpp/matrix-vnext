using System;
using Matrix.Xml;
using Matrix.Xmpp.AdvancedMessageProcessing;

using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.AdvancedMessageProcessing
{
    public class AmpTest
    {
        [Fact]
        public void Test1()
        {
            const string XML1 =
                "<amp xmlns='http://jabber.org/protocol/amp' per-hop='true'/>";
            
            const string XML2 =
                "<amp xmlns='http://jabber.org/protocol/amp' from='bernardo@hamlet.lit/elsinore' to='francisco@hamlet.lit/pda'/>";

            const string XML3 = "<amp xmlns='http://jabber.org/protocol/amp'/>";
            
             
            const string XML4 = "<amp xmlns='http://jabber.org/protocol/amp' status='alert' from='bernardo@hamlet.lit/elsinore' to='francisco@hamlet.lit'/>";

            var amp1 = XmppXElement.LoadXml(XML1) as Amp;
            Assert.Equal(amp1.PerHop, true);

            var amp2 = XmppXElement.LoadXml(XML2) as Amp;
            Assert.Equal(amp2.From.Equals("bernardo@hamlet.lit/elsinore"), true);
            Assert.Equal(amp2.To.Equals("francisco@hamlet.lit/pda"), true);


            var amp3 = new Amp {};
            amp3.ShouldBe(XML3);

            var amp4 = new Amp
            {
                PerHop = true
            };
            amp4.ShouldBe(XML1);

            var amp44 = new Amp
            {
                  Status = Matrix.Xmpp.AdvancedMessageProcessing.Action.Alert,
                  From = "bernardo@hamlet.lit/elsinore",
                  To = "francisco@hamlet.lit"
            };
            amp44.ShouldBe(XML4);
        }
    }
}
