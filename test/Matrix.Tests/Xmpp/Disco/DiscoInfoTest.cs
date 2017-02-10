using System.Linq;
using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Disco;
using Shouldly;

namespace Matrix.Tests.Xmpp.Disco
{
    
    public class DiscoInfoTest
    {
        [Fact]
        public void ElementShouldBeOfTypeDiscoInfo()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.discoinfo1.xml")).ShouldBeOfType<Info>();
        }

        [Fact]
        public void TestHasField1()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.discoinfo1.xml")).Cast<Info>();
            var xdata = info.XData;
            
            xdata.HasField("muc#roominfo_description").ShouldBeTrue();
            xdata.HasField("muc#roominfo_occupants").ShouldBeTrue();
            xdata.HasField("muc#roominfo_description2").ShouldBeFalse();
            xdata.HasField("muc#roominfo_occupants").ShouldBeTrue();
        }

        [Fact]
        public void TestGetField()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.discoinfo1.xml")).Cast<Info>();
            var xdata = info.XData;

            xdata.GetFields().Count().ShouldBe(5);
            xdata.GetField("muc#roominfo_occupants").GetValue().ShouldBe("3");
        }
    }
}
