using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Matrix.Xml.Tests
{
    [TestClass]
    public class XmppXElementTests
    {
        [TestMethod]
        public void TestLoadXml()
        {
            string xml1 = "<a><b>foo</b></a>";
            var elA = XmppXElement.LoadXml(xml1);
            elA.ToString(false).ShouldBe(xml1);
        }
    }
}
