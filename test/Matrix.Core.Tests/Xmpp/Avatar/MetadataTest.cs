namespace Matrix.Tests.Xmpp.Avatar
{

    using Xunit;

    using Matrix.Xml;
    using Shouldly;
    using Matrix.Xmpp.Avatar;

    public class MetadataTests
    {
        [Fact]
        public void TestFactory()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.metadata.xml"))
                .ShouldBeOfType<Metadata>();
        }

        [Fact]
        public void Info_Sub_Element_Should_Exist()
        {
            var metadata = XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.metadata.xml")).Cast<Metadata>();

            var info = metadata.Info;

            info.ShouldNotBeNull();
            info.CountBytes.ShouldBe(123456);
            info.Height.ShouldBe(64);
            info.Width.ShouldBe(64);
            info.Id.ShouldBe("357a8123a30844a3aa99861b6349264ba67a5694");
            info.Uri.ShouldBe(new System.Uri("http://avatars.example.org/happy.gif"));
            info.Type.ShouldBe("image/gif");
        }
    }
}
