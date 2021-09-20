using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Bookmarks;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Bookmarks
{
    public class StorageTest
    {
        [Fact]
        public void XmlShouldBeOfTypeStorage()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.storage1.xml"))
                .ShouldBeOfType<Storage>();
        }

        [Fact]
        public void TestConferenceCount()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.storage1.xml"))
                .Cast<Storage>()
                .GetConferences()
                .Count()
                .ShouldBe(4);
        }
    }
}
