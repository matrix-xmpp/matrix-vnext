namespace BlazorClient.Pages
{
    using Matrix;
    using Matrix.Extensions.Client.Presence;
    using Matrix.Extensions.Client.Roster;
    using Matrix.Transport.WebSocket;
    using Matrix.Xml;
    using Matrix.Xmpp;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using ViewModels;
    using XmppRoster = Matrix.Xmpp.Roster.Roster;


    public partial class Roster
    {
        private Account Account { get; } = new();
        private ObservableCollection<Contact> Contacts { get; } = new();

        private readonly XmppClient xmppClient = new XmppClient(
            conf =>
            {
                conf.UseWebSocketTransport();
                conf.AutoReconnect = true;
            });

        private async Task Connect()
        {
            xmppClient.Jid = Account.Jid;
            xmppClient.Password = Account.Password;

            await xmppClient.ConnectAsync();

            var roster = await xmppClient.RequestRosterAsync();
            if (roster.Query.OfType<XmppRoster>())
            {
                foreach (var rosterItem in roster.Query.Cast<XmppRoster>().GetRoster())
                {
                    Contacts.Add(new Contact { Name = rosterItem.Name, Jid = rosterItem.Jid });
                }
            }
            await xmppClient.SendPresenceAsync(Show.Chat, "free for chat");
        }

        private async void IncrementCount()
        {
            await xmppClient.ConnectAsync();

            var roster = await xmppClient.RequestRosterAsync();
            if (roster.Query.OfType<XmppRoster>())
            {
                foreach (var rosterItem in roster.Query.Cast<XmppRoster>().GetRoster())
                {
                    Contacts.Add(new Contact { Name = rosterItem.Name, Jid = rosterItem.Jid });
                }
            }
            await xmppClient.SendPresenceAsync(Show.Chat, "free for chat");
        }

        private async Task Disconnect()
        {
            await xmppClient.DisconnectAsync();
        }

    }
}
