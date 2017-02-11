using Matrix.Xml;
using Matrix.Xmpp.ResultSetManagement;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.ResultSetManagement
{
    public class ResultSetManagementTest
    {
        [Fact]
        public void ShouldBeOfTypeSet()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.ResultSetManagement.set1.xml")).ShouldBeOfType<Set>();
        }

        [Fact]
        public void TestSet()
        {
            var set = XmppXElement.LoadXml(Resource.Get("Xmpp.ResultSetManagement.set1.xml")).Cast<Set>();
            Assert.Equal(10, set.Maximum);
        }

        [Fact]
        public void TestBuildSet()
        {
            new Set { Maximum = 10 }.ShouldBe(Resource.Get("Xmpp.ResultSetManagement.set1.xml"));
        }

        [Fact]
        public void TestSett2()
        {
            var set = XmppXElement.LoadXml(Resource.Get("Xmpp.ResultSetManagement.set2.xml")).Cast<Set>();
            Assert.Equal(800, set.Count);
            Assert.Equal("peterpan@neverland.lit", set.Last);
            Assert.Equal("stpeter@jabber.org", set.First.Value);
            Assert.Equal(0, set.First.Index);
        }

        [Fact]
        public void TestBuildSett2()
        {
            new Set
            {
                First = new First { Value = "stpeter@jabber.org", Index = 0 },
                Last = "peterpan@neverland.lit",
                Count = 800
            }.ShouldBe(Resource.Get("Xmpp.ResultSetManagement.set2.xml"));
        }
    }
}
