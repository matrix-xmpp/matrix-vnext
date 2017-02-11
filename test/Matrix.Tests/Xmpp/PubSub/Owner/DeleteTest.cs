using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    public class DeleteTest
    {
        [Fact]
        public void BuildPubsibDeleteIq()
        {

            var pIq = new PubSubOwnerIq
            {
                From = "hamlet@denmark.lit/elsinore",
                To = "pubsub.shakespeare.lit",
                Id = "delete1",
                Type = IqType.Set,
                PubSub = { Delete = new Matrix.Xmpp.PubSub.Owner.Delete { Node = "princely_musings" } }
            };

            pIq.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub_delete_iq1.xml"));
        }
    }
}
