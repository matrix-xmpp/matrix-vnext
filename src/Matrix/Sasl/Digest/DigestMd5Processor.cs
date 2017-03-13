using System.Text;
using System.Threading.Tasks;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl.Digest
{
    public class DigestMd5Processor : ISaslProcessor
    {
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.DigestMd5);

            var ret1 = await xmppClient.SendAsync<Failure, Challenge>(authMessage, cancellationToken);

            if (ret1 is Challenge)
            {
                var ret2 = await HandleChallenge(ret1 as Challenge, xmppClient, cancellationToken);
                if (ret2 is Success)
                    return ret2;

                if (ret2 is Challenge)
                    return await HandleChallenge(ret2 as Challenge, xmppClient, cancellationToken);

                return ret2;
            }
            
            return ret1;
        }

        public async Task<XmppXElement> HandleChallenge(Challenge ch, XmppClient xmppClient, CancellationToken cancellationToken)
        {
            byte[] bytes = ch.Bytes;
            string s = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

            var step1 = new Step1(s);

            if (step1.Rspauth == null)
            {
                var s2 = new Step2(step1, xmppClient);

                string message = s2.GetMessage();
                byte[] b = Encoding.UTF8.GetBytes(message);

                return await xmppClient.SendAsync<Failure, Challenge, Success>(new Response {Bytes = b}, cancellationToken);
            }

            return await xmppClient.SendAsync<Failure, Success>(new Response(), cancellationToken);
        }
    }
}
