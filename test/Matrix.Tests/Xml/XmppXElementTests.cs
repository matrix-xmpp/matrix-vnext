using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xml
{
    public class XmppXElementTests
    {
        [Fact]
        public void TestLoadXmlFromString()
        {
            string xml1 = "<a><b>foo</b></a>";
            var elA = XmppXElement.LoadXml(xml1);
            elA.ToString(false).ShouldBe(xml1);
        }
    }
}
