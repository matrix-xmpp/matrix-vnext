using System;
using System.Text;
using System.Threading.Tasks;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl.Plain
{
    public class PlainProcessor : ISaslProcessor
    {
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.Plain, GetMessage(xmppClient));

            return
                await xmppClient.SendAsync<Success, Failure>(authMessage, cancellationToken);
        }

        private string GetMessage(XmppClient xmppClient)
        {
            // NULL Username NULL Password
            var sb = new StringBuilder();
            sb.Append((char)0);
            sb.Append(xmppClient.Username);
            sb.Append((char)0);
            sb.Append(xmppClient.Password);
            byte[] msg = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(msg, 0, msg.Length);
        }
    }
}