using Matrix.Xml;
using Matrix.Xmpp.Capabilities;
using Matrix.Xmpp.Disco;
using Matrix.Xmpp.Stream;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Capabilities
{
    public class CapsTest
    {
        [Fact]
        public void TestBuildHash()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.discoinfo1.xml")).Cast<Info>();
            var hash = Caps.BuildHash(info);
            hash.ShouldBe("q07IKJEyjvHSyhy//CH0CxmKi8w=");
        }

        [Fact]
        public void TestBuildHash2()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.discoinfo3.xml")).Cast<Info>();
            string hash = Caps.BuildHash(info);
            hash.ShouldBe("XH3meI11JZj00/DhOlop7p7jKBE=");
        }

        [Fact]
        public void TestBuildHash5()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.discoinfo4.xml")).Cast<Info>();
            string hash = Caps.BuildHash(info);
            hash.ShouldBe("8ungGR8ouA8Bi9LIUp8r3+1tgbY=");
        }

        [Fact]
        public void TestBuildHash6()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.discoinfo5.xml")).Cast<Info>();
            string hash = Caps.BuildHash(info);
            hash.ShouldBe("KV4qaXUgvEqhWE7WEJoqvO6gTYA=");
        }

        [Fact]
        public void TestCapsInStreamFeatures()
        {
            var features = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.streamfeatures.xml")).Cast<StreamFeatures>();
            var caps = features.Caps;
            caps.Node.ShouldBe("http://jabberd.org");
            caps.Version.ShouldBe("ItBTI0XLDFvVxZ72NQElAzKS9sU=");
            caps.Hash.ShouldBe("sha-1");
        }
    }
}
