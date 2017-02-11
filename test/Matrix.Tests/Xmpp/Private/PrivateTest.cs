using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Private
{
    
    public class PrivateTest
    {
        [Fact]
        public void ShoudBeOfTypePrivate()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Private.private1.xml")).ShouldBeOfType<Matrix.Xmpp.Private.Private>();
        }
    }
}