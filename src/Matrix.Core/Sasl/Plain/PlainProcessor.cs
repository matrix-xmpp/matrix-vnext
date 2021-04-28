using System;
using System.Text;
using System.Threading.Tasks;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl.Plain
{
    /// <summary>
    /// XMPP implementation of SASL PLAIN
    /// </summary>
    public class PlainProcessor : ISaslProcessor
    {
        /// <inheritdoc/>
        public async Task<XmppXElement> AuthenticateClientAsync(IXmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.Plain, GetMessage(xmppClient));

            return
                await xmppClient.SendAsync<Success, Failure>(authMessage, cancellationToken).ConfigureAwait(false);
        }

        private string GetMessage(IXmppClient xmppClient)
        {
            // NULL Username NULL Password
            var sb = new StringBuilder();
            sb.Append((char)0);
            sb.Append(xmppClient.Jid.Local);
            sb.Append((char)0);
            sb.Append(xmppClient.Password);
            byte[] msg = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(msg, 0, msg.Length);
        }
    }
}
