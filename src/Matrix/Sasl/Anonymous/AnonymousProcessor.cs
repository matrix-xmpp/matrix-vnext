using System.Threading.Tasks;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl.Anonymous
{
    public class AnonymousProcessor : ISaslProcessor
    {
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient, CancellationToken cancellationToken)
        {
            /*
               <auth xmlns='urn:ietf:params:xml:ns:xmpp-sasl' mechanism='ANONYMOUS'/>
            */
            var authMessage = new Auth(SaslMechanism.Anonymous);

            return
               await xmppClient.SendAsync<Success, Failure>(authMessage, cancellationToken);
        }
    }
}
