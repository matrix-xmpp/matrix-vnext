using Matrix.Xml;
using Matrix.Xmpp.ResultSetManagement;
using Xunit;

namespace Matrix.Xmpp.Tests.ResultSetManagement
{
    [Collection("Factory collection")]
    public class Test
    {
        private const string XML1 =
            @"<set xmlns='http://jabber.org/protocol/rsm'>
                <max>10</max>
            </set>";

        private const string XML2 = @"<set xmlns='http://jabber.org/protocol/rsm'>
      <first index='0'>stpeter@jabber.org</first>
      <last>peterpan@neverland.lit</last>
      <count>800</count>
    </set>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);

            Assert.Equal(true, xmpp1 is Set);
            var set = xmpp1 as Set;
            Assert.Equal(10, set.Maximum);


            new Set { Maximum = 10 }.ShouldBe(XML1);
        }

        [Fact]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);

            Assert.Equal(true, xmpp1 is Set);
            var set = xmpp1 as Set;
            Assert.Equal(800, set.Count);
            Assert.Equal("peterpan@neverland.lit", set.Last);
            Assert.Equal("stpeter@jabber.org", set.First.Value);
            Assert.Equal(0, set.First.Index);

            var set2 = new Set
                           {
                               First = new First {Value = "stpeter@jabber.org", Index = 0},
                               Last = "peterpan@neverland.lit",
                               Count = 800
                           };

            set2.ShouldBe(XML2);
        }

    }
}
