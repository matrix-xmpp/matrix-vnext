using Matrix.Xml;
using Matrix.Xmpp.IBB;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.IBB
{
    
    public class IBBTest
    {
        [Fact]
        public void ElementSouldBeOfTypeOpen()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open1.xml")).ShouldBeOfType<Open>();
        }

        [Fact]
        public void ElementSouldBeOfTypeClose()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.close1.xml")).ShouldBeOfType<Close>();
        }

        [Fact]
        public void ElementSouldBeOfTypeData()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.data1.xml")).ShouldBeOfType<Data>();
        }

        [Fact]
        public void TestOpenProperties()
        {
            var open = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open1.xml")).Cast<Open>();
            Assert.Equal(open.BlockSize, 4096);
            Assert.Equal(open.Sid, "i781hf64");
            Assert.Equal(open.Stanza, StanzaType.Iq);

            open = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open2.xml")).Cast<Open>();
            Assert.Equal(open.BlockSize, 4096);
            Assert.Equal(open.Sid, "i781hf64");
            Assert.Equal(open.Stanza, StanzaType.Iq);
        }
        
        [Fact]
        public void TestBuildOpen()
        {
            var open = new Open {BlockSize = 4096, Sid = "i781hf64", Stanza = StanzaType.Iq};
            open.ShouldBe(Resource.Get("Xmpp.IBB.open1.xml"));
            
            var open2 = new Open { BlockSize = 4096, Sid = "i781hf64" };
            open2.ShouldBe(Resource.Get("Xmpp.IBB.open2.xml"));
        }

        [Fact]
        public void TestCloseId()
        {
            var close = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.close1.xml")).Cast<Close>();
            Assert.Equal(close.Sid, "i781hf64");
        }

        [Fact]
        public void TestBuildClose()
        {
            var open = new Close {Sid = "i781hf64"};
            open.ShouldBe(Resource.Get("Xmpp.IBB.close1.xml"));
        }

        [Fact]
        public void TestData()
        {
            var data = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.data1.xml")).Cast<Data>();
            Assert.Equal(data.Sid, "i781hf64");
            Assert.Equal(data.Sequence, 99);
        }

        [Fact]
        public void TestBuildData()
        {
            var data = new Data { Sid = "i781hf64", Sequence = 99 };
            data.ShouldBe(Resource.Get("Xmpp.IBB.data1.xml"));
        }
    }
}
