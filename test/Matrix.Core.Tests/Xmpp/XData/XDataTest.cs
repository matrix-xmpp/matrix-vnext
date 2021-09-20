using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.XData;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.XData
{
    public class XDataTest
    {
        [Fact]
        public void XmlShouldBeTypeOfData()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.XData.xdata1.xml"))
                .ShouldBeOfType<Data>();
        }

        [Fact]
        public void XmlShouldBeTypeOfField()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.XData.field1.xml"))
                .ShouldBeOfType<Field>();
        }

        [Fact]
        public void XmlShouldBeTypeOfDataAndContainTypeOfReported()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.XData.xdata1.xml"))
                .Cast<Data>()
                .Element<Reported>()
                .ShouldNotBeNull();
        }

        [Fact]
        public void TestXdataTitle()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.XData.xdata2.xml"))
                .Cast<Data>()
                .Title.ShouldBe("Joogle Search: verona");
        }


        [Fact]
        public void TestXdataItemCount()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.XData.xdata2.xml"))
                .Cast<Data>()
                .GetItems()
                .Count()
                .ShouldBe(5);
        }

        [Fact]
        public void BuildFromWithFields()
        {
            string expectedXml = Resource.Get("Xmpp.XData.xdata3.xml");

            new Data
            {
                Type = FormType.Result,
                Fields = new [] { new Field("var1", "value1"), new Field("var2", "value2") }
            }
            .ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildFieldWithValues()
        {
            string expectedXml = Resource.Get("Xmpp.XData.field1.xml");
            new Field
            {
                Var = "pubsub#children",
                Values = new[] { "queue1", "queue2", "queue3" }
            }
            .ShouldBe(expectedXml);
        }


        [Fact]
        public void TestFieldTypeTextSingle()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.XData.field2.xml"))
                .Cast<Field>()
                .Type
                .ShouldBe(FieldType.TextSingle);
            
        }
        
        [Fact]
        public void TeBuildTextSingleField()
        {
            var xmlExpected = Resource.Get("Xmpp.XData.field2.xml");
                
            new Field
            {
                Type = FieldType.TextSingle,
                Var = "muc#roomconfig_roomname",
                Label = "Natural-Language Room Name"
            }
            .ShouldBe(xmlExpected);
        }
    }
}
