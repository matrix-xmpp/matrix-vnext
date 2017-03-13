using System.Threading.Tasks;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl
{
    public interface IAuthenticate
    {
        Task<XmppXElement> AuthenticateAsync(Mechanisms mechanisms, XmppClient xmppClient, CancellationToken cancellationToken);
    }
}
