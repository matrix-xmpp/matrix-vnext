namespace Matrix
{
    using System;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ReconnectHandler : XmppHandler
    {
        readonly XmppConnection xmppCon;
        readonly CancellationTokenSource cts = new CancellationTokenSource();

        bool reconnecting = false;
        bool shouldReconnect = true;

        public ReconnectHandler(XmppConnection xmppConnection)
            : base(xmppConnection)
        {
            this.xmppCon = xmppConnection;

            xmppCon
                .StateChanged
                .Skip(1)
                .Subscribe(st =>
                {
                    if (st == SessionState.Disconnected
                        && shouldReconnect
                        && !reconnecting)
                    {
                        // got disconnected
                        Task.Run(async () => await Reconnect().ConfigureAwait(false));
                    }
                });

            xmppCon
                .StateChanged
                .Subscribe(se =>
                {
                    if (se == SessionState.Disconnecting)
                    {
                        // intended disconnect by user, no reconnect required
                        this.shouldReconnect = false;
                    }
                });
        }

        private async Task Reconnect()
        {
            reconnecting = true;
            ExponentialBackoff backoff = new ExponentialBackoff();

            while (reconnecting)
            {
                try
                {
                    await backoff.Delay().ConfigureAwait(false);
                    await xmppCon.ConnectAsync(cts.Token).ConfigureAwait(false);

                    reconnecting = false;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public Task Stop()
        {
            return Task.Run(() =>
            {
                cts.Cancel();
                reconnecting = false;
            });
        }
    }
}
