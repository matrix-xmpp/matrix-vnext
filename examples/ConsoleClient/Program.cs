namespace ConsoleClient
{
    using Matrix;
    using Matrix.Extensions.Client.Presence;
    using Matrix.Extensions.Client.Roster;
    using Matrix.Transport.Socket;
    using Matrix.Transport.WebSocket;
    using Matrix.Xmpp;
    using Matrix.Xmpp.Base;
    using System;
    using System.Diagnostics;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var xmppClient = new XmppClient(
                conf =>
                {
                    conf.UseSocketTransport();
                    //conf.UseWebSocketTransport();
                    conf.AutoReconnect = true;

                },
                (handlers, client) => handlers.Add(new XmppLoggingHandler(client)))
            {
                Jid = "user@server.com",
                Password = "***secret***"
            };

            xmppClient.StateChanged.Subscribe(v => {
                Debug.WriteLine($"State changed: {v}");
            });

            xmppClient
                .XmppXElementReceived
                .Where(el => el is Presence)
                .Subscribe(el =>
                {
                    Debug.WriteLine(el.ToString());
                });

            xmppClient
                .XmppXElementReceived
                .Where(el => el is Message)
                .Subscribe(el =>
                {
                    Debug.WriteLine(el.ToString());
                });

            xmppClient
                .XmppXElementReceived
                .Where(el => el is Iq)
                .Subscribe(el =>
                {
                    Debug.WriteLine(el.ToString());
                });

            
            xmppClient
                .StateChanged
                .Where(s => s == SessionState.Binded)
                .Subscribe(async v =>
                {
                    var roster = await xmppClient.RequestRosterAsync();
                    Debug.WriteLine(roster.ToString());
                    await xmppClient.SendPresenceAsync(Show.Chat, "free for chat");
                });

            
            await xmppClient.ConnectAsync();

            //await xmppClient.SendAsync(new Matrix.Xmpp.Client.Message
            //{
            //    Type = MessageType.Chat,
            //    To = "user@server.com",
            //    Body = "Hello World"
            //});

            Console.ReadLine();

            await xmppClient.DisconnectAsync();

            Console.ReadLine();
        }
    }
}
