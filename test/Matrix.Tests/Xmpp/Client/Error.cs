using Xunit;

using Matrix.Xml;
using Shouldly;

namespace Matrix.Tests.Xmpp.Client
{
    public class Error
    {
        [Fact]
        public void TestErrors()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error3.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.Forbidden);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error4.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.Gone);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error5.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.InternalServerError);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error6.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.ItemNotFound);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error7.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.JidMalformed);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error8.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.BadRequest);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error9.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.NotAcceptable);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error10.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.NotAuthorized);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error11.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.NotModified);
            XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error12.xml")).Cast<Matrix.Xmpp.Client.Error>().Condition.ShouldBe(Matrix.Xmpp.Base.ErrorCondition.PaymentRequired);
            
            var err = XmppXElement.LoadXml(Resource.Get("Xmpp.Client.error1.xml")).Cast<Matrix.Xmpp.Client.Error>();
            err.Text.ShouldBe("dummy text");
        }

        [Fact]
        public void BuildError()
        {
            var expectedXml1 = Resource.Get("Xmpp.Client.error1.xml");
            var expectedXml2 = Resource.Get("Xmpp.Client.error2.xml");

            new Matrix.Xmpp.Client.Error(Matrix.Xmpp.Base.ErrorCondition.BadRequest)
            {
                Text = "dummy text"
            }
            .ShouldBe(expectedXml1);

            new Matrix.Xmpp.Client.Error(Matrix.Xmpp.Base.ErrorCondition.BadRequest)
            .ShouldBe(expectedXml2);
        }
    }
}
