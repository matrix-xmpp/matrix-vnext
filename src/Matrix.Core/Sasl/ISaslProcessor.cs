using System.Threading.Tasks;
using Matrix.Xml;
using System.Threading;

namespace Matrix.Sasl
{
    public interface ISaslProcessor
    {
        Task<XmppXElement> AuthenticateClientAsync(IXmppClient xmppClient, CancellationToken cancellationToken);
    }
}
