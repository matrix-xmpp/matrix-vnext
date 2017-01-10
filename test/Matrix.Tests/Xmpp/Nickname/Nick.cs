using Xunit;

using Matrix.Xml;


namespace Matrix.Tests.Xmpp.Nickname
{
    
    public class Nick
    {
        string xml1 = @"<nick xmlns='http://jabber.org/protocol/nick'>Ishmael</nick>";

        string xml2 = @"<presence from='narrator@moby-dick.lit/pda' to='pequod@muc.moby-dick.lit/narrator' xmlns='jabber:client'>
  <show>away</show>
  <status>writing</status>
  <nick xmlns='http://jabber.org/protocol/nick'>Ishmael</nick>
</presence>
";

        [Fact]
        public void Test()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);

            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Nickname.Nick);

            Matrix.Xmpp.Nickname.Nick nick1 = xmpp1 as Matrix.Xmpp.Nickname.Nick;

            Assert.Equal(nick1 == "Ishmael", true);
           
        }

        [Fact]
        public void Test2()
        {            
            Matrix.Xmpp.Nickname.Nick nick1 = "Alex";
            Assert.Equal(nick1.Value, "Alex");
            Assert.Equal(nick1 == "Alex", true);
                       

            Matrix.Xmpp.Nickname.Nick nick2 = new Matrix.Xmpp.Nickname.Nick();
            nick2 = "Ishmael";
            Assert.Equal(nick2.Value, "Ishmael");
            Assert.Equal(nick2 == "Ishmael", true);

            Matrix.Xmpp.Nickname.Nick nick3 = new Matrix.Xmpp.Nickname.Nick("Alex");            
            Assert.Equal(nick3.Value, "Alex");
            Assert.Equal(nick3 == "Alex", true);
        }

        [Fact]
        public void Test3()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml2);

            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Client.Presence);

            Matrix.Xmpp.Client.Presence pres = xmpp1 as Matrix.Xmpp.Client.Presence;

            Matrix.Xmpp.Nickname.Nick nick1 =  pres.Nick;

            Assert.Equal(nick1 == "Ishmael", true);

        }

        [Fact]
        public void Test4()
        {
            Matrix.Xmpp.Client.Presence pres = new Matrix.Xmpp.Client.Presence();
            pres.Nick = "Alex";

            Assert.Equal(pres.Nick.Value, "Alex");
            
            pres.Nick.Value = "Ishmael";
            Assert.Equal(pres.Nick.Value, "Ishmael");
        }

    }
}

