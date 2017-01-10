using Matrix.Xml;
using Xunit;

namespace Matrix.Tests.Xmpp.Mood
{
    
    public class MoodTest
    {
        const string XML1 = @"<pubsub xmlns='http://jabber.org/protocol/pubsub'>
    <publish node='http://jabber.org/protocol/mood'>
      <item>
        <mood xmlns='http://jabber.org/protocol/mood'>
          <annoyed/>
          <text>curse my nurse!</text>
        </mood>
      </item>
    </publish>
  </pubsub>";

        private const string XML2 = @"<mood xmlns='http://jabber.org/protocol/mood'>
          <annoyed/>
          <text>curse my nurse!</text>
        </mood>";

        private const string XML3 = @"<mood xmlns='http://jabber.org/protocol/mood'>
          <in_awe/>          
        </mood>";

        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Mood.Mood);


            var mood = new Matrix.Xmpp.Mood.Mood
                {
                    UserMood = Matrix.Xmpp.Mood.Moods.Annoyed,
                    MoodText = "curse my nurse!"
                };

            mood.ShouldBe(XML2);
            mood.ToPubSub().ShouldBe(XML1);
        }

        [Fact]
        public void Test2()
        {
            var xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Mood.Mood);

            var mood = new Matrix.Xmpp.Mood.Mood
            {
                UserMood = Matrix.Xmpp.Mood.Moods.InAwe,
            };

            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.InAwe, true);
            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.Hungry, false);
            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.InLove, false);

            mood.ShouldBe(XML3);
        }
    }
}
