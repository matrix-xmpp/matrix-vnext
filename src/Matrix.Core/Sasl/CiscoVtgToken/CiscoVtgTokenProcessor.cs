using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix.Sasl.CiscoVtgToken
{
    /// <summary>
    /// XMPP implementation of CISCO VTG TOKEN SASL
    /// </summary>
    public class CiscoVtgTokenProcessor : ISaslProcessor
    {
        string AccessToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CiscoVtgTokenProcessor"/> class.
        /// </summary>
        /// <param name="accessToken">The token</param>
        public CiscoVtgTokenProcessor(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <inheritdoc/>
        public async Task<XmppXElement> AuthenticateClientAsync(IXmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.CiscoVtgToken, GetMessage(xmppClient));

            return
                await xmppClient.SendAsync<Success, Failure>(authMessage, cancellationToken).ConfigureAwait(false);
        }

        internal string GetMessage(IXmppClient xmppClient)
        {
            // Base64(userid=user@domain, NULL, token=one-time-password)
            // userid=juliet@capulet.com/0/token=2345678
            var sb = new StringBuilder();

            sb.Append("userid=");
            sb.Append(xmppClient.Username);
            sb.Append("@");
            sb.Append(xmppClient.XmppDomain);
            
            sb.Append((char)0);
            
            sb.Append("token=");
            sb.Append(AccessToken);

            var msg = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(msg, 0, msg.Length);
        }
    }
}