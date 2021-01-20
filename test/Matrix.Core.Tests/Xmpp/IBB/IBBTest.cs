using Matrix.Xml;
using Matrix.Xmpp.IBB;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.IBB
{
    
    public class IBBTest
    {
        [Fact]
        public void ElementShouldBeOfTypeOpen()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open1.xml")).ShouldBeOfType<Open>();
        }

        [Fact]
        public void ElementShouldBeOfTypeClose()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.close1.xml")).ShouldBeOfType<Close>();
        }

        [Fact]
        public void ElementShouldBeOfTypeData()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.data1.xml")).ShouldBeOfType<Data>();
        }

        [Fact]
        public void TestOpenProperties()
        {
            var open = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open1.xml")).Cast<Open>();
            Assert.Equal(4096, open.BlockSize);
            Assert.Equal("i781hf64", open.Sid);
            Assert.Equal(StanzaType.Iq, open.Stanza);

            open = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open2.xml")).Cast<Open>();
            Assert.Equal(4096, open.BlockSize);
            Assert.Equal("i781hf64", open.Sid);
            Assert.Equal(StanzaType.Iq, open.Stanza);
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
            Assert.Equal("i781hf64", close.Sid);
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
            Assert.Equal("i781hf64", data.Sid);
            Assert.Equal(99, data.Sequence);
        }

        [Fact]
        public void TestBuildData()
        {
            var data = new Data { Sid = "i781hf64", Sequence = 99 };
            data.ShouldBe(Resource.Get("Xmpp.IBB.data1.xml"));
        }
    }
}
