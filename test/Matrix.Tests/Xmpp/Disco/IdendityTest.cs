using System.Collections.Generic;
using Matrix.Xml;
using Xunit;
using Matrix.Xmpp.Disco;
using Shouldly;

namespace Matrix.Tests.Xmpp.Disco
{
    public class IdendityTest
    {
        [Fact]
        public void TestEquals()
        {
            var id = new Identity {Type = "t", Name = "n", Category = "c"};
            var id1 = new Identity { Type = "t", Name = "n", Category = "c" };
            var id2 = new Identity { Type = "t2", Name = "n2", Category = "c2" };

            Assert.Equal(id.Equals(id1), true);
            Assert.Equal(id.Equals(id2), false);

            var list = new List<Identity> {id};

            list.Contains(id).ShouldBeTrue();
            list.Contains(id1).ShouldBeTrue();
            list.Contains(id2).ShouldBeFalse();
        }

        [Fact]
        public void ElementShouldbeOfTypeIdendity()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.idendity1.xml")).ShouldBeOfType<Identity>();
        }

        [Fact]
        public void TestIdendity()
        {
            var identity = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.idendity1.xml")).Cast<Identity>();

            identity.Category.ShouldBe("conference");
            identity.Type.ShouldBe("text");
            identity.Name.ShouldBe("Play-Specific Chatrooms");
        }

        [Fact]
        public void BuildIdendity()
        {
            var expectedXml = Resource.Get("Xmpp.Disco.idendity1.xml");

            Identity identity = new Identity("text", "Play-Specific Chatrooms", "conference");
            identity.ShouldBe(expectedXml);
        }
    }
}
