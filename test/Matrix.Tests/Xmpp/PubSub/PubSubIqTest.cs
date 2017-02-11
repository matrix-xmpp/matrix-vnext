using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class PubSubIqTest
    {
        [Fact]
        public void TestBuildPubSubIq1()
        {
            PubSubIq pIq = new PubSubIq
            {
                Type = Matrix.Xmpp.IqType.Result,
                From = "pubsub.shakespeare.lit",
                To = "hamlet@denmark.lit/elsinore",
                Id = "create2",
                PubSub = {Create = new Matrix.Xmpp.PubSub.Create {Node = "25e3d37dabbab9541f7523321421edc5bfeb2dae"}}
            };


            pIq.ShouldBe(Resource.Get("Xmpp.PubSub.publish_iq1.xml"));
        }

        [Fact]
        public void TestBuildPubSubIq2()
        {
            var pIq = new PubSubIq
            {
                Type = Matrix.Xmpp.IqType.Set,
                From = "hamlet@denmark.lit/elsinore",
                To = "pubsub.shakespeare.lit",
                Id = "create2"

            };

            pIq.PubSub.Create       = new Matrix.Xmpp.PubSub.Create();
            pIq.PubSub.Configure    = new Matrix.Xmpp.PubSub.Configure();
            
            pIq.ShouldBe(Resource.Get("Xmpp.PubSub.publish_iq2.xml"));
        }
    }
}
