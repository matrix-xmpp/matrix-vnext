using Xunit;

using Matrix.Xml;
using Shouldly;
using Matrix.Xmpp.Avatar;
using System.Text;

namespace Matrix.Tests.Xmpp.Avatar
{
    public class DataTests
    {
        [Fact]
        public void TestFactory()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.data.xml"))
                .ShouldBeOfType<Data>();
        }

        [Fact]
        public void TestReadAttributes()
        {
            var data = XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.data.xml")).Cast<Data>();

            data.Bytes.ShouldBe(Encoding.UTF8.GetBytes("Hello World"));
            
        }        
    }
}
