using System.Net.Security;
using System.Threading.Tasks;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Channels;

using Matrix.Sasl;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Sasl;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Tls;

namespace Matrix
{
    public class XmppClient : XmppConnection
    {

        #region << Properties >>

        public string Username { get; set; }

        public string Password { get; set; }

        public string Resource { get; set; } = "MatriX";

        public bool UseStartTls { get; set; } = true;

        public IAuthenticate SalsHandler { get; set; } = new DefaultSaslHandler();
        #endregion




        //private async Task SendStreamHeader()
        //{
        //    var streamHeader = new Stream
        //    {
        //        To = new Jid(XmppDomain),
        //        Version = "1.0"
        //    };

        //    await SendAsync(streamHeader.StartTag());
        //}



        public async Task<IChannel> ConnectAsync()
        {
            //var iChannel = await ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port));
            var iChannel = await _bootstrap.ConnectAsync(XmppDomain, Port);
            //await SendStreamHeader();
            var feat = await SendStreamHeaderAsync();
            await HandleStreamFeaturesAsync(feat);
            return iChannel;
        }

        //        public async Task<bool> CloseAsync()
        //        {
        //            return await CloseAsync(2000);
        //        }


        private async Task HandleStreamFeaturesAsync(StreamFeatures features)
        {
            if (SessionState < SessionState.Securing && features.SupportsStartTls && UseStartTls)
            {
                await HandleStreamFeaturesAsync(await DoStartTlsAsync());
            }
            else if (SessionState < SessionState.Authenticating)
            {
                var authRet = await DoAuthenicateAsync(features.Mechanisms);
                await HandleStreamFeaturesAsync(authRet);
            }
            else if (SessionState < SessionState.Binding)
                await DoBindAsync();
        }

        private async Task<StreamFeatures> DoStartTlsAsync()
        {
            SessionState = SessionState.Securing;
            var tlsHandler =
                new TlsHandler(stream
                => new SslStream(stream,
                true,
                (sender, certificate, chain, errors) => CertificateValidator.RemoteCertificateValidationCallback(sender, certificate, chain, errors)),
                new ClientTlsSettings(XmppDomain));

            var res = await WaitForStanzaHandler.SendAsync<Proceed>(new StartTls());
            base._pipeline.AddFirst(tlsHandler);
            var streamFeatures = await ResetStreamAsync();
            SessionState = SessionState.Secure;

            return streamFeatures;
        }

        private async Task<StreamFeatures> DoAuthenicateAsync(Mechanisms mechanisms)
        {
            SessionState = SessionState.Authenticating;
            var res = await SalsHandler.AuthenticateAsync(mechanisms, this);

            if (res is Success)
            {
                SessionState = SessionState.Authenticated;
                return await ResetStreamAsync();
            }

            return null;
        }

        private async Task<Iq> DoBindAsync()
        {
            SessionState = SessionState.Binding;
            var bIq = new BindIq { Type = IqType.Set, Bind = { Resource = Resource } };
            var resIq = await IqHandler.SendIqAsync(bIq);
            SessionState = SessionState.Binded;
            return resIq as Iq;
        }
    }
}
