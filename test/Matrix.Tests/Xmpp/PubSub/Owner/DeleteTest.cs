using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    public class DeleteTest
    {
        private const string XML1 = @" <iq type='set'
                xmlns='jabber:client'
                from='hamlet@denmark.lit/elsinore'
                to='pubsub.shakespeare.lit'
                id='delete1'>
              <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
                <delete node='princely_musings'/>
              </pubsub>
            </iq>";

        [Fact]
        public void Test1()
        {

            var pIq = new PubSubOwnerIq
            {
                From = "hamlet@denmark.lit/elsinore",
                To = "pubsub.shakespeare.lit",
                Id = "delete1",
                Type = IqType.Set,
                PubSub = { Delete = new Matrix.Xmpp.PubSub.Owner.Delete { Node = "princely_musings" } }
            };

            pIq.ShouldBe(XML1);
        }
    }
}
