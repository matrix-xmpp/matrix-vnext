using Matrix.Xml;
using Shouldly;
using System.Text.RegularExpressions;
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

        [Fact]
        public void XmlShouldContainOneCDataElement()
        {
            var el = XmppXElement.LoadXml(Resource.Get("Xml.cdata1.xml"));

            var xml = el.ToString(false);
            Regex.Matches(xml, "CDATA").Count.ShouldBe(1);
        }
    }
}
