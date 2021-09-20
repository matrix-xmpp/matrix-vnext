namespace Matrix.Tests.Xmpp.MessageArchiveManagement
{
    using Matrix.Xml;
    using Matrix.Xmpp.Client;
    using Matrix.Xmpp.MessageArchiveManagement;
    using Shouldly;
    using Xunit;

    public class ResultTest
    {
        string MAM_MESSAGE = @"<message xmlns='jabber:client' id='aeb213' to='juliet@capulet.lit/chamber'>
  <result xmlns='urn:xmpp:mam:2' queryid='f27' id='28482-98726-73623'>
    <forwarded xmlns='urn:xmpp:forward:0'>
      <delay xmlns='urn:xmpp:delay' stamp='2010-07-10T23:08:25Z'/>
      <message xmlns='jabber:client' from='witch@shakespeare.lit' to='macbeth@shakespeare.lit'>
        <body>Hail to thee</body>
        </message>
        </forwarded>
        </result>
        </message>";

        string MAM_RESULT = @"<result xmlns='urn:xmpp:mam:2' queryid='f27' id='28482-98726-73623' />";

        [Fact]
        public void ShouldBeOfTypeResult()
        {
            XmppXElement.LoadXml(MAM_MESSAGE).Cast<Message>().FirstElement.ShouldBeOfType<Result>();
        }

        [Fact]
        public void TestIds()
        {
            XmppXElement.LoadXml(MAM_MESSAGE).Cast<Message>().Element<Result>().QueryId.ShouldBe("f27");
            XmppXElement.LoadXml(MAM_MESSAGE).Cast<Message>().Element<Result>().Id.ShouldBe("28482-98726-73623");

            new Result
                {
                    QueryId = "f27",
                    Id = "28482-98726-73623"
                }
                .ShouldBe(MAM_RESULT);
        }
    }
}
