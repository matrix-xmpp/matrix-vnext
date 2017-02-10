using Matrix.Xml;
using Matrix.Xmpp.MessageCarbons;
using Xunit;

using Shouldly;

namespace Matrix.Tests.Xmpp.MessageCarbons
{
    
    public class MessageCarbonTest
    {
        [Fact]
        public void ShouldBeOfTypeForwarded()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.MessageCarbons.forwarded1.xml")).ShouldBeOfType<Forwarded>();
        }

        [Fact]
        public void MessageShouldComtainForwarded()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.MessageCarbons.message_carbons1.xml"));

            msg.FirstXmppXElement.Cast<ForwardContainer>()
                .Forwarded.ShouldNotBeNull();
        }
    }
}
