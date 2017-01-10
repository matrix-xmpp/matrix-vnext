using System.Threading.Tasks;
using Matrix.Xml;

namespace Matrix.Sasl
{
    public interface ISaslProcessor
    {
        Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient);
    }
}
