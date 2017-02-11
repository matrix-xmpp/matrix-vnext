using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class RetractTest
    {
        [Fact]
        public void ShoudBeOfTypeRetract()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.retract1.xml")).ShouldBeOfType<Retract>();
        }
     
        [Fact]
        public void TestRetract()
        {
            var retract = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.retract1.xml")).Cast<Retract>();
            Assert.Equal(retract.Node, "princely_musings");
            IEnumerable<Item> items = retract.GetItems();
            Assert.Equal(items.Count(), 3);
            Assert.Equal(items.ToArray()[0].Id, "ae890ac52d0df67ed7cfdf51b644e901");
            Assert.Equal(items.ToArray()[1].Id, "abc");
            Assert.Equal(items.ToArray()[2].Id, "def");
        }
    }
}
