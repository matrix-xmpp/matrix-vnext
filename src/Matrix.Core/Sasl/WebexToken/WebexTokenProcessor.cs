namespace Matrix.Sasl.WebexToken
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Sasl;
    using Xml;
    using Xmpp.Sasl;

    /// <summary>
    /// XMPP implementation of CISCO Webex TOKEN SASL
    /// </summary>
    public class WebexTokenProcessor : ISaslProcessor
    {
        string AccessToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebexTokenProcessor"/> class.
        /// </summary>
        /// <param name="accessToken">The token</param>
        public WebexTokenProcessor(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <inheritdoc/>
        public async Task<XmppXElement> AuthenticateClientAsync(IXmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.WebexToken, GetMessage());

            return
                await xmppClient.SendAsync<Success, Failure>(authMessage, cancellationToken).ConfigureAwait(false);
        }      
   
        internal string GetMessage()
        {
            // TODO, check if this is correct
            var sb = new StringBuilder();
           
            sb.Append(AccessToken);

            var msg = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(msg, 0, msg.Length);
        }
    }
}