using Matrix.Xml;
using Xunit;


namespace Matrix.Tests.Xmpp.Tune
{
    
    public class TuneTest
    {
        const string XML1 = @"<tune xmlns='http://jabber.org/protocol/tune'>
          <artist>Yes</artist>
          <length>686</length>
          <rating>8</rating>
          <source>Yessongs</source>
          <title>Heart of the Sunrise</title>
          <track>3</track>
          <uri>http://www.yesworld.com/lyrics/Fragile.html#9</uri>
        </tune>";
        
        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Tune.Tune);

            var tune1 = xmpp1 as Matrix.Xmpp.Tune.Tune;
            Assert.Equal(tune1.Artist, "Yes");
            Assert.Equal(tune1.Length, 686);
            Assert.Equal(tune1.Rating, 8);
            Assert.Equal(tune1.Source, "Yessongs");
            Assert.Equal(tune1.Title, "Heart of the Sunrise");
            Assert.Equal(tune1.Track, "3");
            Assert.Equal(tune1.Uri.ToString(), "http://www.yesworld.com/lyrics/Fragile.html#9");

            var tune = new Matrix.Xmpp.Tune.Tune
            {
                Artist = "Yes",
                Length = 686,
                Rating = 8,
                Source = "Yessongs",
                Title = "Heart of the Sunrise",
                Track = "3",
                Uri = new System.Uri("http://www.yesworld.com/lyrics/Fragile.html#9")
            };

            tune.ShouldBe(XML1);
        }

      
    }
}
