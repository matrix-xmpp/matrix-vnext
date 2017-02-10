using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Mood
{
    public class MoodTest
    {
        [Fact]
        public void ShouldBeOfTypeMood()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Mood.mood1.xml")).ShouldBeOfType<Matrix.Xmpp.Mood.Mood>();
        }

        [Fact]
        public void TestBuildMood()
        {
            var mood = new Matrix.Xmpp.Mood.Mood
            {
                UserMood = Matrix.Xmpp.Mood.Moods.Annoyed,
                MoodText = "curse my nurse!"
            };

            mood.ShouldBe(Resource.Get("Xmpp.Mood.mood1.xml"));
        }

        [Fact]
        public void TestBuildMood2()
        {
            var mood = new Matrix.Xmpp.Mood.Mood
            {
                UserMood = Matrix.Xmpp.Mood.Moods.InAwe,
            };

            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.InAwe, true);
            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.Hungry, false);
            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.InLove, false);

            mood.ShouldBe(Resource.Get("Xmpp.Mood.mood2.xml"));
        }

        [Fact]
        public void TestMoodToPubsub()
        {
            var mood = new Matrix.Xmpp.Mood.Mood
            {
                UserMood = Matrix.Xmpp.Mood.Moods.Annoyed,
                MoodText = "curse my nurse!"
            };
            mood.ToPubSub().ShouldBe(Resource.Get("Xmpp.Mood.pubsub1.xml"));
        }
    }
}
