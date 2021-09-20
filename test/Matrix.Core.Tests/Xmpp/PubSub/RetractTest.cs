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
            Assert.Equal("princely_musings", retract.Node);
            IEnumerable<Item> items = retract.GetItems();
            Assert.Equal(3, items.Count());
            Assert.Equal("ae890ac52d0df67ed7cfdf51b644e901", items.ToArray()[0].Id);
            Assert.Equal("abc", items.ToArray()[1].Id);
            Assert.Equal("def", items.ToArray()[2].Id);
        }
    }
}
