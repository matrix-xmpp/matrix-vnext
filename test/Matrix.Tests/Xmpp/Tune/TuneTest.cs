using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Tune
{
    public class TuneTest
    {
        [Fact]
        public void XmlShouldBeOfTypeTune()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .ShouldBeOfType<Matrix.Xmpp.Tune.Tune>();
        }

        [Fact]
        public void TestArtist()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Artist.ShouldBe("Yes");
        }

        [Fact]
        public void TestLength()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Length.ShouldBe(686);
        }

        [Fact]
        public void TestRating()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Rating.ShouldBe(8);
        }

        [Fact]
        public void TestSource()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Source.ShouldBe("Yessongs");
        }

        [Fact]
        public void TestTrack()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Track.ShouldBe("3");
        }

        [Fact]
        public void TestUri()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Uri.ToString().ShouldBe("http://www.yesworld.com/lyrics/Fragile.html#9");
        }

        [Fact]
        public void BuildTune()
        {
            var expectedXml = Resource.Get("Xmpp.Tune.tune1.xml");
            new Matrix.Xmpp.Tune.Tune
            {
                Artist = "Yes",
                Length = 686,
                Rating = 8,
                Source = "Yessongs",
                Title = "Heart of the Sunrise",
                Track = "3",
                Uri = new System.Uri("http://www.yesworld.com/lyrics/Fragile.html#9")
            }
            .ShouldBe(expectedXml);
        }
    }
}
