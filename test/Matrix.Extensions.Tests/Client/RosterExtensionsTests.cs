namespace Matrix.Extensions.Tests.Client
{  
    using Xunit;

    using Matrix.Extensions.Client.Roster;
    using System.Threading.Tasks;

    public class RosterExtensionsTests
    {
        private readonly EchoClient echoClient = new EchoClient();

        [Fact]
        public async Task Request_Full_RosterTest()
        {
            string expectedResult = "<iq xmlns='jabber:client' type='get' id='foo'><query xmlns='jabber:iq:roster'/></iq>";

            var rosterIq = await echoClient.RequestRosterAsync();
            rosterIq.Id = "foo";

            // assert
            rosterIq.ShouldBe(expectedResult);
        }

        [Fact]
        public async Task Request_Roster_With_Given_Version_Test()
        {
            string expectedResult = "<iq xmlns='jabber:client' type='get' id='foo'><query xmlns='jabber:iq:roster' ver='99'/></iq>";
                        
            var rosterIq = await echoClient.RequestRosterAsync("99");
            rosterIq.Id = "foo";

            // assert
            rosterIq.ShouldBe(expectedResult);
        }
    }
}
