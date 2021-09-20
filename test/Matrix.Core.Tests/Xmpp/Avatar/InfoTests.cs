using Xunit;

using Matrix.Xml;
using Shouldly;
using Matrix.Xmpp.Avatar;

namespace Matrix.Tests.Xmpp.Avatar
{
    
    public class InfoTests
    {
        [Fact]
        public void TestFactory()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.info.xml"))
                .ShouldBeOfType<Info>();           
        }

        [Fact]
        public void TestReadAttributes()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.info.xml")).Cast<Info>();

            info.CountBytes.ShouldBe(123456);
            info.Height.ShouldBe(64);
            info.Width.ShouldBe(64);
            info.Id.ShouldBe("357a8123a30844a3aa99861b6349264ba67a5694");
            info.Uri.ShouldBe(new System.Uri("http://avatars.example.org/happy.gif"));
            info.Type.ShouldBe("image/gif");        
        }

        [Fact]
        public void TestBuildInfo()
        {
            var expectedXml = Resource.Get("Xmpp.Avatar.info.xml");
            new Info
            {
                CountBytes = 123456,
                Height = 64,
                Width = 64,
                Type = "image/gif",
                Uri = new System.Uri("http://avatars.example.org/happy.gif"),
                Id = "357a8123a30844a3aa99861b6349264ba67a5694"                
            }
            .ShouldBe(expectedXml);
        }
    }
}
