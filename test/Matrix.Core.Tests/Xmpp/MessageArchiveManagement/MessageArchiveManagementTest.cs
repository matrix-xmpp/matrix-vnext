namespace Matrix.Tests.Xmpp.MessageArchiveManagement
{
    using Matrix.Xml;
    using Matrix.Xmpp.Client;
    using Matrix.Xmpp.MessageArchiveManagement;
    using Shouldly;
    using Xunit;

    public class MessageArchiveManagementTest
    {
        string MAM_QUERY = @"<iq xmlns='jabber:client' type='set' id='juliet1'>
                                    <query xmlns='urn:xmpp:mam:2' queryid='f27' node='fdp/submitted/capulet.lit/sonnets' />
                                </iq>";
        [Fact]
        public void ShouldBeOfTypeMessageArchive()
        {
            XmppXElement.LoadXml(MAM_QUERY).Cast<Iq>().Query.ShouldBeOfType<MessageArchive>();
        }

        [Fact]
        public void TestQueryId()
        {
            XmppXElement.LoadXml(MAM_QUERY).Cast<Iq>().Query.Cast<MessageArchive>().QueryId.ShouldBe("f27");
        }


        [Fact]
        public void TestQueryNode()
        {
            XmppXElement.LoadXml(MAM_QUERY).Cast<Iq>().Query.Cast<MessageArchive>().Node.ShouldBe("fdp/submitted/capulet.lit/sonnets");
        }

        [Fact]
        public void TestBuildMamQuery()
        {
            var mamQuery = new IqQuery<MessageArchive>
            {
                Type = Matrix.Xmpp.IqType.Set,
                Id = "juliet1", 
                Query =
                {
                    QueryId = "f27",
                    Node = "fdp/submitted/capulet.lit/sonnets"
                }
            };
            mamQuery.ShouldBe(MAM_QUERY);
        }
    }
}
