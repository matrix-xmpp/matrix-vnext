using Xunit;


using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Muc.Admin;

namespace Matrix.Tests.Xmpp.Muc.Admin
{
    
    public class AdminIqTest
    {
        private const string XML1 = @"<iq xmlns='jabber:client' id='1'>
            <query xmlns='http://jabber.org/protocol/muc#admin'>
                <item nick='pistol' role='none'>
                    <reason>my reason!</reason>
                </item>
            </query>
        </iq>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Iq);

            var iq = xmpp1 as Iq;
            if (iq != null)
                Assert.Equal(true, iq.Query is AdminQuery);
        }

        [Fact]
        public void Test2()
        {
            var aIq = new AdminIq();
            aIq.AdminQuery.AddItem(new Item(Matrix.Xmpp.Muc.Role.None, "pistol", "my reason!"));
            aIq.Id = "1";
            aIq.ShouldBe(XML1);
        }
    }
}