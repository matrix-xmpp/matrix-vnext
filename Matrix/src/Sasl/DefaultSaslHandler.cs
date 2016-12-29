using System.Threading.Tasks;
using Matrix.Sasl.Digest;
using Matrix.Sasl.Plain;
using Matrix.Sasl.Scram;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;

namespace Matrix.Sasl
{
    public class DefaultSaslHandler : IAuthenticate
    {
        public async Task<XmppXElement> AuthenticateAsync(Mechanisms mechanisms, XmppClient xmppClient)
        {
            ISaslProcessor saslProc = null;
            if (mechanisms.SupportsMechanism(SaslMechanism.ScramSha1))
                saslProc = new ScramSha1Processor();

            if (mechanisms.SupportsMechanism(SaslMechanism.DigestMd5))
                saslProc = new DigestMd5Processor();

            else if (mechanisms.SupportsMechanism(SaslMechanism.Plain))
                saslProc = new PlainProcessor();

            return await saslProc.AuthenticateClientAsync(xmppClient);
        }
    }
}
