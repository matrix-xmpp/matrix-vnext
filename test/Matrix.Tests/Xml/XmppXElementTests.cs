using Xunit;
using Matrix.Xml;
using Matrix.Xmpp;
using Shouldly;

namespace Matrix.Tests.Xml
{
    public class XmppXElementTests
    {
        [Fact]
        public void TestLoadXml()
        {
            string xml1 = "<a><b>foo</b></a>";
            var elA = XmppXElement.LoadXml(xml1);
            elA.ToString(false).ShouldBe(xml1);
        }
    }
}
