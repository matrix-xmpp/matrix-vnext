using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Base
{
    public class XmppXElementWithJidAttributeTest
    {
        [Fact]
        public void JidShouldNotBeNull()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Base.mucuser2.xml"))
                .Cast<Matrix.Xmpp.Muc.User.X>()
                .Item.Jid.ShouldNotBeNull();
        }

        [Fact]
        public void TestJid()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Base.mucuser2.xml"))
                .Cast<Matrix.Xmpp.Muc.User.X>()
                .Item.Jid.ToString().ShouldBe("server");
        }

        [Fact]
        public void JidShouldBeNull()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Base.mucuser1.xml"))
                .Cast<Matrix.Xmpp.Muc.User.X>()
                .Item.Jid.ShouldBeNull();
            

        }
    }
}
