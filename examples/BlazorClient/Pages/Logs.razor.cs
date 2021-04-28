namespace BlazorClient.Pages
{
    using Matrix;
    using Matrix.Extensions.Client.Presence;
    using Matrix.Extensions.Client.Roster;
    using Matrix.Transport.WebSocket;
    using Matrix.Xmpp;

    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using ViewModels;

    public partial class Logs
    {
        private Account Account { get; } = new();
        private ObservableCollection<LogMessage> LogMessages { get; } = new();

        readonly XmppClient xmppClient;

        public Logs()
        {
            xmppClient = new XmppClient(
                conf =>
                {
                    conf.UseWebSocketTransport();
                    conf.AutoReconnect = true;
                }
            );
            
            xmppClient
                .Transport
                .XmlReceived
                .Subscribe(el =>
                {
                    InvokeAsync(()=>
                    {
                        LogMessages.Add(new LogMessage() {Direction = "RECV", Xml = el.ToString()});
                        StateHasChanged();
                    });
                    
                });

            xmppClient
                .Transport
                .XmlSent
                .Subscribe(el =>
                {
                    InvokeAsync(() =>
                    {
                        LogMessages.Add(new LogMessage() {Direction = "SEND", Xml = el.ToString()});
                        StateHasChanged();
                    });
                });

        }

        private async Task Connect()
        {
            var jid = new Jid(Account.Jid);

            xmppClient.Username = jid.Local;
            xmppClient.XmppDomain = jid.Domain;
            xmppClient.Password = Account.Password;

            await xmppClient.ConnectAsync();
            await xmppClient.RequestRosterAsync();
            await xmppClient.SendPresenceAsync(Show.Chat, "free for chat");
        }

        private async Task Disconnect()
        {
            await xmppClient.DisconnectAsync();
        }
    }
}