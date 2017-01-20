using Matrix.Xml;
using Matrix.Xmpp.Bosh;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Bosh
{
     
    public class BoshTest
    {
        [Fact]
        public void XmppXElementShouldbeOfTypeBody()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh1.xml")).ShouldBeOfType<Body>();
        }

        [Fact]
        public void TestCondition()
        {
            var body = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh1.xml")).Cast<Body>();
            body.Condition.ShouldBe(Condition.RemoteConnectionFailed);

            var body2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh2.xml")).Cast<Body>();
            body2.Condition.ShouldBe(Condition.ItemNotFound);

            var body3 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh3.xml")).Cast<Body>();
            body3.Condition.ShouldBe(Condition.HostUnknown);

            var body4 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh4.xml")).Cast<Body>();
            body4.Condition.ShouldBe(Condition.SeeOtherUri);

            var body5 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh5.xml")).Cast<Body>();
            body5.Condition.ShouldBe(Condition.BadRequest);
        }

        [Fact]
        public void BuildBody()
        {
            var expectedXml = Resource.Get("Xmpp.Bosh.bosh1.xml");
            new Body
                {
                    Type = Type.Terminate,
                    Condition = Condition.RemoteConnectionFailed
                }
                .ShouldBe(expectedXml);

            var expectedXml2 = Resource.Get("Xmpp.Bosh.bosh2.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.ItemNotFound
            }
            .ShouldBe(expectedXml2);

            var expectedXml3 = Resource.Get("Xmpp.Bosh.bosh3.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.HostUnknown
            }
            .ShouldBe(expectedXml3);

            var expectedXml4 = Resource.Get("Xmpp.Bosh.bosh4.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.SeeOtherUri
            }
            .ShouldBe(expectedXml4);

            var expectedXml5 = Resource.Get("Xmpp.Bosh.bosh5.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.BadRequest
            }
            .ShouldBe(expectedXml5);
        }
    }
}
